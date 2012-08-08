using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class MazeBuilder : MonoBehaviour {

    
	public int sizeX; // number of pieces to place along X
    public int sizeZ; // number of pieces to place along Z
	public int numberPieces;

    public int originX; // starting coordinate in X
    public int originZ; // starting coordinate in Z

    public float originY; // starting coordinate in Y to place pieces (note that this is typically half of piece height)
    public float offsetY; // offset for the collectible layer and bubble.

    public float pieceSizeX; // set these to the total size of the pieces to be placed
    public float pieceSizeZ; // used for offsetting the next instantiate

    public Transform[] MazePieces; // set this in the editor to include all the prefabs to choose between
    public Transform startRoom;
    public Transform endRoom;
	
	public Transform collectible;
    public Transform hazard;
    public Transform bigBucks;

    public int bigBucksMax;
    
	
	public Transform character;

    public int smartStartX;
    public int smartStartZ;

    public int smartEndX;
    public int smartEndZ;
	bool hasEnd;

    [HideInInspector]
    public float percentChanceDeployable = 50;
    [HideInInspector]
    public float percentChanceBonanza = 50;
    [HideInInspector]
    public float percentChanceReplaceCashWithMine = 10;
    [HideInInspector]
    public float percentChanceReplaceSoloCashWithBigBucks;

    enum Dirs {N, S, E, W};
    bool[,] free;
	GameObject[,] maze;
    int[,] serialiser;

    [HideInInspector]
    public string preload = @""; 

    List<DeployableTracker> currentDeployables;
    

    private class DeployableTracker
    {
        public Transform collectible;
        public Vector3 position;

        public DeployableTracker(Transform collect, Vector3 pos)
        {
            this.collectible = collect;
            this.position = pos;
        }

        public string ToString()
        {
            return collectible.name + " " + position.x + " " + position.y + " " + position.z;
        }

    }

    // Use this for initialization
	void Start () {

        destroyMaze();
        createSmartMaze();
        /*Logging logger = GameObject.FindGameObjectWithTag("Logger").GetComponent<Logging>();
        logger.setSessionID();
        logger.logMessage("Session started");
        logger.logSystemInfo();*/
		
	}
	
	void OnGUI () {
   		
	}
	
	// Update is called once per frame
	void Update () {
    		
	}

    public void resetMaze()
    {
		destroyMaze();
        //createRandomMaze();
        createSmartMaze();
    }

    void destroyMaze()
    {
        GameObject[] pieces = GameObject.FindGameObjectsWithTag("MazePiece");
        foreach (GameObject piece in pieces)
        {
        	Destroy(piece);
        }
		pieces = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject piece in pieces)
        {
        	Destroy(piece);
        }
		pieces = GameObject.FindGameObjectsWithTag("Collectable");
		foreach (GameObject piece in pieces)
        {
        	Destroy(piece);
        }
//		return;
    }

    private void createSmartMaze()
    {
        hasEnd = false; // flag that an end has been positioned.
        int smartStartXActual = smartStartX;
        int smartStartZActual = smartStartZ;
        if (smartStartX == -1)
        {
            smartStartXActual = Random.Range(0, sizeX);
        }
        if (smartStartZ == -1)
        {
            smartStartZActual = Random.Range(0, sizeZ);
        }
        bool valid = false;

        //this.currentDeployables = new List<DeployableTracker>();

        while (!valid)
        {
			//yield return new WaitForEndOfFrame();
            Vector3 location = new Vector3(originX + (smartStartXActual * pieceSizeX), originY, originZ + (smartStartZActual * pieceSizeZ));
            free = new bool[sizeX, sizeZ];
			maze = new GameObject[sizeX, sizeZ];
	        for(int i = 0; i < sizeX; i++)
	        {
	            for (int j = 0; j < sizeZ; j++)
	            {
	                free[i,j] = true;
					maze[i,j] = null;
	            }
	        }
			Transform trans = Instantiate(startRoom, location, transform.rotation) as Transform;
            GameObject start = trans.gameObject;
            MazePiece mp = (MazePiece)start.GetComponent("MazePiece");
            mp.setCoord(smartStartXActual, smartStartZActual);
            //Debug.Log("Coords = " + smartStartXActual + ", " + smartStartZActual);
            free[smartStartXActual, smartStartZActual] = false;
			maze[smartStartXActual, smartStartZActual] = start;

            List<GameObject> open = new List<GameObject>();
            open.Add(start);
            int counter = 0;
            while (counter < this.numberPieces && open.Count > 0)
            {
				GameObject old = open[0];
                MazePiece oldMP = old.GetComponent<MazePiece>();
				List<Dirs> connections = new List<Dirs>();
	            if (oldMP.N)
	            {
	                connections.Add(Dirs.N);
	            }
	            if (oldMP.S)
	            {
	                connections.Add(Dirs.S);
	            }
	            if (oldMP.E)
	            {
	                connections.Add(Dirs.E);
	            }
	            if (oldMP.W)
	            {
	                connections.Add(Dirs.W);
	            }
	
	            foreach (Dirs d in connections)
	            {
					//yield return new WaitForSeconds(1);
                
					Transform newPiece;
					int newX = 0;
					int newZ = 0;
					
					if (d == Dirs.N)
	                {
	                    //need a tile north of here
	
	                    // find an appropriate tile
	                    newPiece = selectAppropriatePiece(oldMP, Dirs.N);
	                    newX = oldMP.getX();
	                    newZ = oldMP.getZ() - 1;
					}
					else if (d == Dirs.S)
	                {
	                    // need a tile south of here
	                    newPiece = selectAppropriatePiece(oldMP, Dirs.S);
	                    newX = oldMP.getX();
	                    newZ = oldMP.getZ() + 1;
					}
					else if (d == Dirs.E)
	                {
	                    // need a tile east of here
	                    newPiece = selectAppropriatePiece(oldMP, Dirs.E);
	                    newX = oldMP.getX() - 1;
	                    newZ = oldMP.getZ();
					}
					else //(d == Dirs.W)
	                {
	                    // need a tile west of here
	                    newPiece = selectAppropriatePiece(oldMP, Dirs.W);
	                    newX = oldMP.getX() + 1;
	                    newZ = oldMP.getZ();
					}
					if(!free[newX, newZ])
					{
						// if we get here, bad shit be happening
						Debug.Log("ERROR IN PLACEMENT - TILE COLLISION : " + newX + ", " + newZ);
					}
	                location = new Vector3(originX + (newX * pieceSizeX), originY, originZ + (newZ * pieceSizeZ));
	                Transform newTrans = Instantiate(newPiece, location, transform.rotation) as Transform;
					
					//Debug.Log("Coords = " + newX + ", " + newZ);
	                GameObject newObject = newTrans.gameObject;
					MazePiece newMP = newObject.GetComponent<MazePiece>();
					if(newMP.is3D)
					{
						newTrans.Rotate(Vector3.right * 90, Space.World);
					}
	                newMP.setCoord(newX, newZ);
	                free[newX, newZ] = false;
					maze[newX, newZ] = newObject;
					// clear flags on the new MP for all directions that have a mazepiece adjacent
					if(newZ-1 >= 0 && !free[newX,newZ-1])
					{
						// clear flag 
						newMP.clearN();
						
					}
					if(newZ + 1 < sizeZ && !free[newX,newZ+1])
					{
						// clear flag
						newMP.clearS();
					}
					if(newX-1 >= 0 && !free[newX-1,newZ])
					{
						// clear flag
						newMP.clearE();
					}
					if(newX+1 < sizeX && !free[newX+1, newZ])
					{
						// clear flag
						newMP.clearW();
					}
						
					
					
					
	                open.Add(newObject);
                	// add it in to open
				}
                
                open.RemoveAt(0);
                counter++;
            }
			//Debug.Log("Applying Caps");
			//outputFree();
			
			this.applyCaps();
			//Debug.Log("Placing End");
			
            this.findEnds();
            //valid = validateMaze();
            //if (!valid)
            //{
			//	this.destroyMaze();
            //}
			valid = true;
        }
		//outputFree();
        //yield return new WaitForEndOfFrame();
		//instantiateChar(smartStartXActual, smartStartZActual);
		//placeDeployables();
        //string output = this.ToString();

        //Logging log = GameObject.FindGameObjectWithTag("Logger").GetComponent<Logging>();
        //log.logLevel(output);

        //Debug.Log(output);
        
        //FromString(output);

    }

	/*void instantiateChar (int xOffset, int zOffset)
	{
		Vector3 location = new Vector3(originX + (xOffset * pieceSizeX), originY + offsetY, originZ + (zOffset * pieceSizeZ));
		Transform chr = Instantiate(this.character, location, transform.rotation) as Transform;
		GridMove mover = chr.GetComponentInChildren<GridMove>();
		mover.setDestination(originX + (smartEndX * pieceSizeX), originZ + (smartEndZ * pieceSizeZ));
	}*/

    bool checkRandom(float percentChance)
    {
        int rand = Random.Range(0, 100);
        if (rand < percentChance)
        {
            return true;
        }
        return false;
    }

    void placeDeployables()
	{
        int bigBucksCounter = 0;
		foreach(GameObject obj in maze)
		{
			if(obj != null)
			{
				MazePiece mp = obj.GetComponent<MazePiece>();
				if(mp.isRoom)
				{
					int xOffset = mp.getX();
					int zOffset = mp.getZ();
					if(checkRandom(percentChanceDeployable))
					{
                        if(checkRandom(percentChanceBonanza))
                        {
                            // bonanza room
						    Vector3 location = new Vector3(originX + (xOffset * pieceSizeX), originY + offsetY, originZ + (zOffset * pieceSizeZ));
                            Transform deploy;
                            if (checkRandom(percentChanceReplaceCashWithMine))
                            {
                                deploy = this.hazard;
                            }
                            else
                            {
                                deploy = this.collectible;
                            }
						    Instantiate(deploy, location + new Vector3(1,0,1), transform.rotation);
                            currentDeployables.Add(new DeployableTracker(deploy, location + new Vector3(1, 0, 1)));
                            if (checkRandom(percentChanceReplaceCashWithMine))
                            {
                                deploy = this.hazard;
                            }
                            else
                            {
                                deploy = this.collectible;
                            }
						    Instantiate(deploy, location + new Vector3(1,0,-1), transform.rotation);
                            currentDeployables.Add(new DeployableTracker(deploy, location + new Vector3(1, 0, -1)));
                            if (checkRandom(percentChanceReplaceCashWithMine))
                            {
                                deploy = this.hazard;
                            }
                            else
                            {
                                deploy = this.collectible;
                            }
						    Instantiate(deploy, location + new Vector3(-1f,0,1f), transform.rotation);
                            currentDeployables.Add(new DeployableTracker(deploy, location + new Vector3(-1, 0, 1f)));
                            if (checkRandom(percentChanceReplaceCashWithMine))
                            {
                                deploy = this.hazard;
                            }
                            else
                            {
                                deploy = this.collectible;
                            }
						    Instantiate(deploy, location + new Vector3(-1f,0,-1f), transform.rotation);
                            currentDeployables.Add(new DeployableTracker(this.collectible, location + new Vector3(-1f, 0, -1f)));
                        }
                        else
                        {
                            // one in the middle
                            Transform deploy;
                            if (checkRandom(percentChanceReplaceCashWithMine))
                            {
                                deploy = this.hazard;
                            }
                            else
                            {
                                if (checkRandom(percentChanceReplaceSoloCashWithBigBucks) && bigBucksCounter < bigBucksMax)
                                {
                                    deploy = this.bigBucks;
                                    bigBucksCounter++;
                                }
                                else
                                {
                                    deploy = this.collectible;
                                }
                            }
                            Vector3 location = new Vector3(originX + (xOffset * pieceSizeX), originY + offsetY, originZ + (zOffset * pieceSizeZ));
                            Instantiate(deploy, location, transform.rotation);
                            currentDeployables.Add(new DeployableTracker(deploy, location));
                        }
                    }
				}
			}
		}
		
	}
	
	private bool validateMaze()
    {
		int ends = countEnds();
        //Debug.Log("Maze ends " + ends);
        if (ends < 7)
        {
			Debug.Log("Insufficient deadends (estimator of branches)");
            return false;
        }
		GameObject[] pieces = GameObject.FindGameObjectsWithTag("MazePiece");
        if(pieces.Length < 12)
		{
			Debug.Log("Insufficient pieces");
			return false;
		}
		foreach(GameObject go in pieces)
		{
			MazePiece mp = go.GetComponent<MazePiece>();
			if(!mp.closed())
			{
				int X = mp.getX();
				int Z = mp.getZ();
				string name = go.name;
				Debug.Log("Incomplete piece : " + name + " (" + X + ", " + Z + ")");
				return false;
			}
		}
		if(!hasEnd)
		{
			Debug.Log("No end room");
			return false;
		}
		if(getManhattanDistance() < 4)
		{
			Debug.Log("Start and End too close");
			return false;
		}
        return true;
    }
	
	int getManhattanDistance()
	{
		int diffX = Mathf.Abs(smartEndX - smartStartX);
		int diffY = Mathf.Abs(smartEndZ - smartStartZ);
		return diffX + diffY;
	}
    
    int countEnds()
    {
        int counter = 0;
        GameObject[] pieces = GameObject.FindGameObjectsWithTag("MazePiece");
        MazePiece mp;
        foreach (GameObject piece in pieces)
        {
            mp = piece.GetComponent<MazePiece>();
            if (mp.isTerminal)
            {
                counter++;
            }
        }
        return counter;
    }

    void findEnds()
    {
        GameObject[] pieces = GameObject.FindGameObjectsWithTag("MazePiece");
        List<GameObject> endCaps = new List<GameObject>();
        MazePiece mp;
        foreach (GameObject piece in pieces)
        {
            mp = piece.GetComponent<MazePiece>();
            if (mp.isTerminal)
            {
                endCaps.Add(piece);
            }
        }
		if(endCaps.Count != 0)
		{
	        int rand = Random.Range(0, endCaps.Count);
	        GameObject oldCap = endCaps[rand];
	        mp = oldCap.GetComponent<MazePiece>();
	        int X = mp.getX();
	        int Z = mp.getZ();
	        string type = oldCap.name;
	        Destroy(oldCap);
	        Quaternion rot = transform.rotation;
            bool hideCuff = false;
	        if (type.Equals("capN(Clone)")) // This is still hardcoded to names. FIX
	        {
	            // rotate 180
	            rot = rot * Quaternion.AngleAxis(180, Vector3.up);
	        }
	        if (type.Equals("capS(Clone)"))
	        {
                hideCuff = true;
	        }
	        if (type.Equals("capE(Clone)"))
	        {
	            // rotate 90 (which way?)
	            rot = rot * Quaternion.AngleAxis(-90, Vector3.up);
                hideCuff = true;
	        }
	        if (type.Equals("capW(Clone)"))
	        {
	            // rotate 90 (other way)
	            rot = rot * Quaternion.AngleAxis(90, Vector3.up);
	        }
	        Vector3 location = new Vector3(originX + (X * pieceSizeX), originY, originZ + (Z * pieceSizeZ));
	        Transform eroom = Instantiate(this.endRoom, location, rot) as Transform;
            /*if (hideCuff)
            {
                Debug.Log("Hiding the cuff (maybe)");
                eroom.gameObject.GetComponentInChildren<CuffHider>().hideCuff();
            }*/
			MazePiece eroomMP = eroom.GetComponent<MazePiece>();
            maze[X, Z] = eroom.gameObject;
			eroomMP.setCoord(X, Z);
			eroomMP.clearS();
			hasEnd = true;
			this.smartEndX = X;
			this.smartEndZ = Z;
		}
        

    }
	
	Transform selectAppropriatePiece(MazePiece mp, Dirs d)
	{
		int X = mp.X;
        int Z = mp.Z;
        if (d == Dirs.N)
        {
            // Set X and Z to new location
            Z = Z - 1;
            
        }
        if (d == Dirs.S)
        {
            Z = Z + 1;
            
        }
        if (d == Dirs.E)
        {
            X = X - 1;
            
        }
        if (d == Dirs.W)
        {
            X = X + 1;
            
        }

		if(!free[X,Z])
		{
			Debug.Log("ERROR PIECE COLLISION AT " + X + ", " + Z);
		}
        
        Transform next = MazePieces[Random.Range(0, MazePieces.Length)];

        bool forceNorth = false;
		bool canNorth = false;
		if(Z-1 >= 0)
		{
	        if (maze[X, Z - 1] != null)
	        {
				
	            forceNorth = maze[X, Z - 1].GetComponent<MazePiece>().S;
	        }
			else
			{
				canNorth = true;
				
			}
		}
        bool forceWest = false;
		bool canWest = false;
		if(X+1 < sizeX)
		{
	        if (maze[X + 1, Z] != null)
	        {
				forceWest = maze[X + 1, Z].GetComponent<MazePiece>().E;
				
	        }
			else
			{
				canWest = true;
			}
		}
        bool forceEast = false;
		bool canEast = false;
		if(X -1 >= 0)
		{
	        if (maze[X - 1, Z] != null)
	        {
	            forceEast = maze[X - 1, Z].GetComponent<MazePiece>().W;
	        }
			else
			{
				canEast = true;
			}
		}
        bool forceSouth = false;
		bool canSouth = false;
		if(Z+1 < sizeZ)
		{
	        if (maze[X, Z + 1] != null)
	        {
	            forceSouth = maze[X, Z + 1].GetComponent<MazePiece>().N;
	        }
			else
			{
				canSouth = true;
			}
		}
		//next = MazePieces[Random.Range(0, MazePieces.Length)];
        //MazePiece nextMp = next.GetComponent<MazePiece>();
		List<Transform> Possibles = new List<Transform>();
		foreach(Transform mps in MazePieces)
		{						
			MazePiece nextMp = mps.GetComponent<MazePiece>();
            if( ( nextMp.N && (forceNorth || canNorth))  || (!nextMp.N && !forceNorth) )
            {
				if( ( nextMp.S && (forceSouth || canSouth))  || (!nextMp.S && !forceSouth) )
            	{
					if( ( nextMp.E && (forceEast || canEast)) || (!nextMp.E && !forceEast) )
            		{
						if( ( nextMp.W && (forceWest || canWest)) || (!nextMp.W && !forceWest))
            			{
                			Possibles.Add(mps);
						}
					}
				}
            }
		}
		if(Possibles.Count == 0)
		{
			Debug.Log("ERROR NO POSSIBLE PIECES HERE " + X + ", " + Z);
		}
		else
		{
			next = Possibles[Random.Range(0, Possibles.Count)];
		}

        
        if (d == Dirs.N)
        {
            mp.clearN();
        }
        if (d == Dirs.S)
        {
            mp.clearS();
        }
        if (d == Dirs.E)
        {
            mp.clearE();
        }
        if (d == Dirs.W)
        {
            mp.clearW();
        }
		if(forceNorth)
		{
			maze[X, Z - 1].GetComponent<MazePiece>().clearS();
		}
		if(forceSouth)
		{
			maze[X, Z + 1].GetComponent<MazePiece>().clearN();
		}
		if(forceEast)
		{
			maze[X - 1, Z].GetComponent<MazePiece>().clearW();
		}
		if(forceWest)
		{
			maze[X + 1, Z ].GetComponent<MazePiece>().clearE();
		}
		return next;
	}
    
	void applyCaps()
    {
        GameObject[] pieces = GameObject.FindGameObjectsWithTag("MazePiece");
        foreach (GameObject piece in pieces)
        {
            MazePiece mp = piece.GetComponent<MazePiece>();
            List<Dirs> connections = new List<Dirs>();
            if (mp.N)
            {
                connections.Add(Dirs.N);
            }
            if (mp.S)
            {
                connections.Add(Dirs.S);
            }
            if (mp.E)
            {
                connections.Add(Dirs.E);
            }
            if (mp.W)
            {
                connections.Add(Dirs.W);
            }

            foreach (Dirs d in connections)
            {
				

                int X = mp.X;
                int Z = mp.Z;
                if (d == Dirs.N)
                {
                    // Set X and Z to new location
                    Z = Z - 1;
                    
                }
                if (d == Dirs.S)
                {
                    Z = Z + 1;
                    
                }
                if (d == Dirs.E)
                {
                    X = X - 1;
                    
                }
                if (d == Dirs.W)
                {
                    X = X + 1;
                    
                }

				if(!free[X,Z])
				{
					Debug.Log("ERROR PIECE COLLISION AT " + X + ", " + Z);
				}
                
                Transform next = MazePieces[Random.Range(0, MazePieces.Length)];
                bool ok = false;

                bool joinNorth = false;
                if (Z-1 >= 0 && maze[X, Z - 1] != null)
                {
                    joinNorth = maze[X, Z - 1].GetComponent<MazePiece>().S;
                }

                bool joinWest = false;
                if (X+1 < sizeX && maze[X + 1, Z] != null)
                {
                    joinWest = maze[X + 1, Z].GetComponent<MazePiece>().E;
                }
                bool joinEast = false;
                if (X-1 >= 0 && maze[X - 1, Z] != null)
                {
                    joinEast = maze[X - 1, Z].GetComponent<MazePiece>().W;
                }
                bool joinSouth = false;
                if (Z + 1 < sizeZ && maze[X, Z + 1] != null)
                {
                    joinSouth = maze[X, Z + 1].GetComponent<MazePiece>().N;
                }

                while (!ok)
                {
					//next = MazePieces[Random.Range(0, MazePieces.Length)];
                    //MazePiece nextMp = next.GetComponent<MazePiece>();
					List<Transform> Possibles = new List<Transform>();
					foreach(Transform mps in MazePieces)
					{						
						MazePiece nextMp = mps.GetComponent<MazePiece>();
	                    if (nextMp.S == joinSouth && nextMp.N == joinNorth && nextMp.E == joinEast && nextMp.W == joinWest)
	                    {
	                        Possibles.Add(mps);
	                    }
					}
					if(Possibles.Count == 0)
					{
						Debug.Log("ERROR NO POSSIBLE PIECES HERE " + X + ", " + Z);
						ok = true;
					}
					else
					{
						next = Possibles[Random.Range(0, Possibles.Count)];
						ok = true;
					}

                }
                // instatiate piece
                Vector3 location = new Vector3(originX + (X * pieceSizeX), originY, originZ + (Z * pieceSizeZ));
                Transform newTrans = Instantiate(next, location, transform.rotation) as Transform;
                //Debug.Log("Coords = " + newX + ", " + newZ);
                GameObject newObject = newTrans.gameObject;

                MazePiece newMP = newObject.GetComponent<MazePiece>();
                if(newMP.is3D)
				{
					newTrans.Rotate(Vector3.right * 90, Space.World);
				}
                newMP.setCoord(X, Z);
                free[X, Z] = false;
                maze[X, Z] = newObject;
				
				if (d == Dirs.N)
                {
                    mp.clearN();
					newMP.clearS();
                }
                if (d == Dirs.S)
                {
                    mp.clearS();
					newMP.clearN();
                }
                if (d == Dirs.E)
                {
                    mp.clearE();
					newMP.clearW();
                }
                if (d == Dirs.W)
                {
                    mp.clearW();
					newMP.clearE();
                }
				if(joinNorth)
				{
					maze[X, Z - 1].GetComponent<MazePiece>().clearS();
					newMP.clearN();
				}
				if(joinSouth)
				{
					maze[X, Z + 1].GetComponent<MazePiece>().clearN();
					newMP.clearS();
				}
				if(joinEast)
				{
					maze[X - 1, Z].GetComponent<MazePiece>().clearW();
					newMP.clearE();
				}
				if(joinWest)
				{
					maze[X + 1, Z ].GetComponent<MazePiece>().clearE();
					newMP.clearW();
				}
            }
        }
    }

    public void replicateMazebuilder(MazeBuilder template)
    {
        

    }

    public string ToString()
    {
        string output = "#dimensions\n";
        output += maze.GetLength(1) + " " + maze.GetLength(0) + "\n";
        output += "#maze\n";
        Quaternion endRoomRotation = new Quaternion();
        for (int i = 0; i < maze.GetLength(1); i++)
        {
            for (int j = 0; j < maze.GetLength(0); j++)
            {
                if (maze[j, i] != null)
                {
                    GameObject thisPiece = maze[j, i];
                    if (thisPiece.name.Equals(this.startRoom.name + "(Clone)"))
                    {
                        output += "S ";
                    }
                    if (thisPiece.name.Equals(this.endRoom.name + "(Clone)"))
                    {
                        output += "E ";
                        endRoomRotation = thisPiece.transform.rotation;
                        
                    }
                    //Debug.Log("Piece - " + thisPiece.name);
                    for (int k = 0; k < MazePieces.Length; k++)
                    {
                        Transform trans = MazePieces[k];
                        //Debug.Log("Transform - " + trans.name);
                        if(thisPiece.name.Equals(trans.name + "(Clone)"))
                        {
                            output += k + " ";
                            break;
                        }
                    }
                }
                else
                {
                    output += "-1 ";
                }
            }
            output += "\n";
        }
        output += "#deployable\n";
        foreach (DeployableTracker deployed in currentDeployables)
        {
            output += deployed.ToString() + "\n";
        }
        output += "#deployableend\n";
        output += "#endRoomRotation\n";
        output += endRoomRotation.x + " " + endRoomRotation.y + " " + endRoomRotation.z + " " + endRoomRotation.w + "\n";
        output += "#end";
        return output;
    }

    public void FromString(string description)
    {

        destroyMaze();

        originY = 0.5f; 
        string[] delim = { "\n" };
        string[] lines = description.Split(delim, StringSplitOptions.None);
        if (!lines[0].Equals("#dimensions"))
        {
            Debug.Log("Badness in string");
            return;
        }
        string[] thisLine = lines[1].Split();
        sizeZ = int.Parse(thisLine[0]);
        sizeX = int.Parse(thisLine[1]);
        free = new bool[sizeX, sizeZ];
        maze = new GameObject[sizeX, sizeZ];
        GameObject end = new GameObject();
        GameObject start = new GameObject();
        if (!lines[2].Equals("#maze"))
        {
            Debug.Log("Badness in string");
            return;
        }
        for (int i = 0; i < sizeX; i++)
        {
            //Debug.Log(lines[i]);
            thisLine = lines[i+3].Split();
            
            for (int j = 0; j < sizeZ; j++)
            {
                //Debug.Log(thisLine[j]);
                if (thisLine[j].Equals("S"))
                {
                    // instantiate Start
                    Transform newPiece = this.startRoom;
                    Vector3 location = new Vector3(originX + (j * pieceSizeX), originY, originZ + (i * pieceSizeZ));
                    Transform newTrans = Instantiate(newPiece, location, transform.rotation) as Transform;
                    GameObject newObject = newTrans.gameObject;
                    start = newObject;
                    maze[j,i] = newObject;
                    MazePiece newMP = newObject.GetComponent<MazePiece>();
                    if (newMP.is3D)
                    {
                        newTrans.Rotate(Vector3.right * 90, Space.World);
                    }
	                
                }
                else if (thisLine[j].Equals("E"))
                {
                    // Instantiate End
                    Transform newPiece = this.endRoom;
                    Vector3 location = new Vector3(originX + (j * pieceSizeX), originY, originZ + (i * pieceSizeZ));
                    Transform newTrans = Instantiate(newPiece, location, transform.rotation) as Transform;
                    GameObject newObject = newTrans.gameObject;
                    end = newObject;
                    maze[j,i] = newObject;
                    MazePiece newMP = newObject.GetComponent<MazePiece>();
                    if (newMP.is3D)
                    {
                        //newTrans.Rotate(Vector3.right * 90, Space.World);
                    }
	                
                }
                else
                {
                    int next = int.Parse(thisLine[j]);
                    if (next == -1)
                    {
                        free[j, i] = true;
                        maze[j, i] = null;
                    }
                    else
                    {
                        free[j, i] = false;
                        Transform newPiece = MazePieces[next];
                        Vector3 location = new Vector3(originX + (j * pieceSizeX), originY, originZ + (i * pieceSizeZ));
                        Transform newTrans = Instantiate(newPiece, location, transform.rotation) as Transform;
                        GameObject newObject = newTrans.gameObject;
                        maze[j,i] = newObject;
                        MazePiece newMP = newObject.GetComponent<MazePiece>();
                        if (newMP.is3D)
                        {
                            newTrans.Rotate(Vector3.right * 90, Space.World);
                        }
	                
	                
                    }
                }
            }
        }
        if (!lines[sizeX+3].Equals("#deployable"))
        {
            Debug.Log("Badness in string");
            return;
        }
        int counter = sizeX + 4;
        this.currentDeployables = new List<DeployableTracker>();
        while (!lines[counter].Equals("#deployableend"))
        {
            string[] components = lines[counter].Split();
            float x = float.Parse(components[1]);
            float y = float.Parse(components[2]);
            float z = float.Parse(components[3]);
            Vector3 location = new Vector3(x,originY + offsetY,z);
            Instantiate(this.collectible, location, transform.rotation);
            currentDeployables.Add(new DeployableTracker(this.collectible, location));
            counter++;
        }
        counter++;
        if (!lines[counter].Equals("#endRoomRotation"))
        {
            Debug.Log("Badness in string");
            return;
        }
        counter++;
        string[] quarternions = lines[counter].Split();
        Quaternion endRoomRotation = new Quaternion(float.Parse(quarternions[0]), float.Parse(quarternions[1]), float.Parse(quarternions[2]), float.Parse(quarternions[3]));
        end.transform.rotation = endRoomRotation;
        Vector3 pos = new Vector3(start.transform.position.x, originY + offsetY, start.transform.position.z);
        Transform chr = Instantiate(this.character, pos, transform.rotation) as Transform;
        //GridMove mover = chr.GetComponentInChildren<GridMove>();
        //mover.setDestination(end.transform.position.x, end.transform.position.z);

            
        
        
        
    }
}

