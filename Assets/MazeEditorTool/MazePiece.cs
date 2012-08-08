using UnityEngine;
using System.Collections;

public class MazePiece : MonoBehaviour {
	
	public bool N;
	public bool E;
	public bool S;
	public bool W;
	
	bool existsN;
	bool existsE;
	bool existsS;
	bool existsW;
	
	public int X;
	public int Z;
	
	public bool isTerminal;
	public bool is3D;
	public bool isRoom;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void setX(int X)
    {
        this.X = X;
    }

    public int getX()
    {
        return X;
    }

    public void setZ(int Z)
    {
        this.Z = Z;
    }

    public int getZ()
    {
        return Z;
    }


    public void setCoord(int X, int Z)
    {
        this.X = X;
        this.Z = Z;
    }

    public void clearN()
    {
		if(N)
			existsN = true;
		N = false;

    }
    public void clearS()
    {
		if(S)
			existsS = true;
        S = false;
    }
    public void clearE()
    {
		if(E)
			existsE = true;
        E = false;
    }
    public void clearW()
    {
		if(W)
			existsW = true;
        W = false;
    }
	
	public bool closed()
	{
		if(!N && !S && !E && !W)
		{
			return true;
		}
		return false;
	}
}
