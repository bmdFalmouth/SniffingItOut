/* Copyright (c) 2007-2008 Technicat, LLC */
/* Minor Editing ACFalk */
// a square plane used for walls, floor, and ceiling

import System.IO;

public var Level:int;

var filePath:String;
var sw:StreamWriter;
var tile:Transform;
var tile2:Transform;
var tile3:Transform;
var player:Transform;
var end:Transform;
var pickup:Transform;


var tileSize:int = 10;

// next state
// Changed this to bypass the deprecated methods transitioning to Unity 2.6 : ACF
var next:Transform;

// maze dimensions
var height:int = 12;
var width:int = 12;

private var maze;
private var tileCounter = 0;

private var facingDown:Quaternion = Quaternion.Euler(180,0,0);

public var moundArray:Transform[];
class Room {
    var east:Transform;
    var west:Transform;
    var north:Transform;
    var south:Transform;
}

function Start() {
    /*maze = new Array(width*height);
    yield StartCoroutine("InstantiateFloor");
    yield StartCoroutine("InstantiateWalls");
    //guiText.enabled = false;
    if (next != null) {
        next.gameObject.active = true;
       
    }*/

     // Added a Call to actually Make your Maze : ACF
     //MakeMaze();
     //SpawnPlayer();
     if(Application.isEditor)
     	filePath = Application.dataPath+"/Level"+Level+".txt";
     else
     	filePath = "Levels/Level"+Level+".txt";
     ReadFile(filePath);
     //ResetFile(filePath);
     //SaveLevel();
     //NetworkInterchange();
     
}
function SaveLevel()
{
	OpenStream(filePath);
	var mazeObjects = GameObject.FindGameObjectsWithTag("MazeWall");
	
	for(var piece in mazeObjects)
	{
		WriteObject(filePath,1,new Vector3(piece.transform.position.x,piece.transform.position.y,piece.transform.position.z),new Vector3(piece.transform.rotation.eulerAngles.x,piece.transform.rotation.eulerAngles.y,piece.transform.rotation.eulerAngles.z));
	}
	
	
	var mazeObjects2 = GameObject.FindGameObjectsWithTag("MazeFloor1");
	
	for(var piece2 in mazeObjects2)
	{
		WriteObject(filePath,2,new Vector3(piece2.transform.position.x,piece2.transform.position.y,piece2.transform.position.z),new Vector3(piece2.transform.rotation.eulerAngles.x,piece2.transform.rotation.eulerAngles.y,piece2.transform.rotation.eulerAngles.z));
	}
	
	var mazeObjects3 = GameObject.FindGameObjectsWithTag("MazeFloor2");
	
	for(var piece3 in mazeObjects3)
	{
		WriteObject(filePath,3,new Vector3(piece3.transform.position.x,piece3.transform.position.y,piece3.transform.position.z),new Vector3(piece3.transform.rotation.eulerAngles.x,piece3.transform.rotation.eulerAngles.y,piece3.transform.rotation.eulerAngles.z));
	}
	var mazeObjects4 = GameObject.FindGameObjectsWithTag("Mound");
	
	for(var piece4 in mazeObjects4)
	{
		WriteObject(filePath,4,new Vector3(piece4.transform.position.x,piece4.transform.position.y,piece4.transform.position.z),new Vector3(piece4.transform.rotation.eulerAngles.x,piece4.transform.rotation.eulerAngles.y,piece4.transform.rotation.eulerAngles.z));
	}
	CloseStream();
	
}
function NetworkInterchange()
{
	var mazeObjects = GameObject.FindGameObjectsWithTag("MazeWall");
	
	for(var piece in mazeObjects)
	{
		Instantiate(tile, new Vector3(piece.transform.position.x,piece.transform.position.y+20,piece.transform.position.z),piece.transform.rotation);
	}
	
	var mazeObjects2 = GameObject.FindGameObjectsWithTag("MazeFloor1");
	
	for(var piece2 in mazeObjects2)
	{
		Instantiate(tile2, new Vector3(piece2.transform.position.x,piece2.transform.position.y+20,piece2.transform.position.z),piece2.transform.rotation);
	}
	
	var mazeObjects3 = GameObject.FindGameObjectsWithTag("MazeFloor2");
	
	for(var piece3 in mazeObjects3)
	{
		Instantiate(tile3, new Vector3(piece3.transform.position.x,piece3.transform.position.y+20,piece3.transform.position.z),piece3.transform.rotation);
	}
	
}
function InstantiateWalls() {
    var mid = tileSize/2;
    var pos = new Vector3(0,3,0);
    for (var x:int =0; x< width; ++x) {
        var xpos:int = x*tileSize;
        for (var y:int =0; y< height; ++y) {
            var ypos:int = y*tileSize;
            var room = new Room();
            pos.x = xpos;
            pos.z = ypos-mid;
            room.south = InstantiateTile(pos,Quaternion.identity);
            room.south.Rotate(90,0,0);
            pos.x = xpos-mid;
            pos.z = ypos;
            room.west = InstantiateTile(pos,Quaternion.identity);
            room.west.Rotate(0,0,270);
            room.west.Rotate(0,90,0);
            pos.x = xpos;
            pos.z = ypos+mid;
            room.north = InstantiateTile(pos,Quaternion.identity);
            room.north.Rotate(270,0,0);
            room.north.Rotate(0,180,0);
            pos.x = xpos+mid;
            pos.z = ypos;
            room.east =InstantiateTile(pos,Quaternion.identity);
            room.east.Rotate(0,0,90);
            room.east.Rotate(0,270,0);
            maze[MazeIndex(x,y)]=room;
            ShowProgress();
        }
    }
}

// floor and ceiling
function InstantiateFloor() {

	var flipY:System.Boolean = false;
	var flipX:System.Boolean = false;

    var floorpos:Vector3 = Vector3.zero;
    for (var x:int=0; x< width; ++x) 
    {
    	if(!flipY)
            {
            	flipX = false;
            }
            else
            {
            	flipX = true;
            }
        floorpos.x = x*tileSize;
        for (var y:int=0; y< height; ++y) {
            floorpos.z = y*tileSize;
            floorpos.y = 0;
            if(!flipX)
            {
            	InstantiateGroundTile1(floorpos,Quaternion.identity);
            }
            else
            {
            	InstantiateGroundTile2(floorpos,Quaternion.identity);
            }
            floorpos.y = tileSize;
            if(!flipX)
            {
            	flipX = true;
            }
            else
            {
            	flipX = false;
            }
            //InstantiateGroundTile(floorpos,facingDown);
            ShowProgress();
        }
        if(!flipY)
        {
        	flipY = true;
        }
        else
        {
        	flipY = false;
        }
    }
}

function ShowProgress() {
    var progress:float = tileCounter/(height*width*6.0);
    //guiText.text = progress.ToString("maze generated: #0%");   
}

function InstantiateTile(pos:Vector3,rot:Quaternion):Transform {
    ++tileCounter;
    return Instantiate(tile,pos,rot);
}
function InstantiateGroundTile1(pos:Vector3,rot:Quaternion):Transform {
    ++tileCounter;
    return Instantiate(tile2,pos,rot);
}
function InstantiateGroundTile2(pos:Vector3,rot:Quaternion):Transform {
    ++tileCounter;
    return Instantiate(tile3,pos,rot);
}

function SpawnPlayer()
{
	Instantiate(player, new Vector3(0,3,0),Quaternion.identity);
	Instantiate(end,new Vector3(110,0,110),Quaternion.identity);
	
	moundArray = new Transform[5];
	
	var positionsX = new Array();
	var positionsY = new Array();
	
	for(var j:int=0; j<5; j++)
	{ 
		positionsX[j] = 0;
		positionsY[j] = 0;
	}
	for(var i:int=0; i<5; i++)
	{
		
		
		var placed:System.Boolean = false;
		while(!placed)
		{
		var xPos:int = (UnityEngine.Random.Range (0,12)*10);
		var zPos:int = (UnityEngine.Random.Range (0,12)*10);
		for(var x:int=0; x<5; x++)
		{
			var possXX:int = xPos - positionsX[x];
			var possXY:int = xPos - positionsY[x];
			var possYX:int = zPos - positionsX[x];
			var possYY:int = zPos - positionsY[x];
			if(possXX<0)
			{
				possXX*=-1;
			}
			if(possXY<0)
			{
				possXY*=-1;
			}
			if(possYX<0)
			{
				possYX*=-1;
			}
			if(possYY<0)
			{
				possYY*=-1;
			}
			if(possXX<=2 || possXY <=2 || possYX<=2||possYY<=2)
			{
				placed = false;
			}
			else
			{
				placed = true;
			}
		}
		}
		var mound:Transform = Instantiate(pickup, new Vector3(xPos,0,zPos),Quaternion.identity);
		moundArray[i] = mound;
	}
}

function MakeMaze() {
    ClearMaze();
    SetOuterWalls();
   
    SubDivideMaze(0,width-1,0,height-1);
   
}

function ClearMaze() {
    for (var x:int=0; x< width; ++x) {
        for (var y:int=0; y< height; ++y) {
            maze[MazeIndex(x,y)].west.active = false;
            maze[MazeIndex(x,y)].east.active = false;
            maze[MazeIndex(x,y)].north.active = false;
            maze[MazeIndex(x,y)].south.active = false;
        }
    }
}

function SubDivideMaze(left,right,bottom,top) {
    if (left!=right && bottom != top) {
        var x:int = Random.Range(left,right);
        var leftdoor:int = Random.Range(left,x+1);
        var rightdoor:int = Random.Range(x+1,right+1);
        var y:int = Random.Range(bottom,top);
        var bottomdoor:int = Random.Range(bottom,y+1);
        var topdoor:int = Random.Range(y+1,top+1);
        AddNorthWall(left,right,y);
        AddEastWall(bottom,top,x);
        var doors = Random.value;
        if (doors < 0.25) {
            SetNorthWall(MazeIndex(leftdoor,y),false);
            SetNorthWall(MazeIndex(rightdoor,y),false);
            SetEastWall(MazeIndex(x,bottomdoor),false);
        } else {
            if (doors < 0.5) {
                SetNorthWall(MazeIndex(leftdoor,y),false);
                SetNorthWall(MazeIndex(rightdoor,y),false);
                SetEastWall(MazeIndex(x,topdoor),false);
            } else {
                    if (doors < 0.75) {
                        SetNorthWall(MazeIndex(rightdoor,y),false);
                        SetEastWall(MazeIndex(x,bottomdoor),false);
                        SetEastWall(MazeIndex(x,topdoor),false);
                    } else {
                            SetNorthWall(MazeIndex(leftdoor,y),false);
                            SetEastWall(MazeIndex(x,bottomdoor),false);
                            SetEastWall(MazeIndex(x,topdoor),false);
                    }
            }
        }
        SubDivideMaze(left,x,y+1,top);
        SubDivideMaze(x+1,right,y+1,top);
        SubDivideMaze(left,x,bottom,y);
        SubDivideMaze(x+1,right,bottom,y);
    }
}

function SetOuterWalls() {
    AddNorthWall(0,width-1,height-1);
    AddSouthWall(0,width-1,0);
    AddEastWall(0,height-1,width-1);
    AddWestWall(0,height-1,0);
    SetNorthWall(MazeIndex(width-1,height-1),false);
}

function MazeIndex(x:int,y:int):int {
    return y*width+x;
}


function SetNorthWall(room,value) {
    maze[room].north.active = value;
    var neighbor:int = RoomNorth(room);
    if (neighbor !=-1) {
        maze[neighbor].south.active = value;
    }
}

function SetSouthWall(room,value) {
    maze[room].south.active = value;
    var neighbor:int = RoomSouth(room);
    if (neighbor !=-1) {
        maze[neighbor].north.active = value;
    }
}

function SetEastWall(room,value) {
    maze[room].east.active = value;
    var neighbor:int = RoomEast(room);
    if (neighbor !=-1) {
        maze[neighbor].west.active = value;
    }
}

function SetWestWall(room,value) {
    maze[room].west.active = value;
    var neighbor:int = RoomWest(room);
    if (neighbor !=-1) {
        maze[neighbor].east.active = value;
    }
}

function AddNorthWall(left:int,right:int,y:int) {
    for (var hwall:int = left; hwall<=right; ++hwall) {
            SetNorthWall(MazeIndex(hwall,y),true);
        }
}

function AddEastWall(bottom:int,top:int,x:int) {
    for (var vwall:int = bottom; vwall<=top; ++vwall) {
        SetEastWall(MazeIndex(x,vwall),true);
    }
}

function AddSouthWall(left:int,right:int,y:int) {
    for (var hwall:int = left; hwall<=right; ++hwall) {
        SetSouthWall(MazeIndex(hwall,y),true);
    }
}

function AddWestWall(bottom:int,top:int,x:int) {
    for (var vwall:int = bottom; vwall<=top; ++vwall) {
        SetWestWall(MazeIndex(x,vwall),true);
    }
}

function RoomEast(index:int) {
    var y:int = index/width;
    var x:int = index-y*width;
    if (x==width-1) {
        return -1;
    } else {
        return MazeIndex(x+1,y);
    }
}

function RoomWest(index:int) {
    var y:int = index/width;
    var x:int = index-y*width;
    if (x==0) {
        return -1;
    } else {
        return MazeIndex(x-1,y);
    }
}

function RoomNorth(index:int) {
    var y:int = index/width;
    var x:int = index-y*width;
    if (y==height-1) {
        return -1;
    } else {
        return MazeIndex(x,y+1);
    }
}

function RoomSouth(index:int) {
    var y:int = index/width;
    var x:int = index-y*width;
    if (y==0) {
        return -1;
    } else {
        return MazeIndex(x,y-1);
    }
}

function GetRoom(x:int,y:int) {
    return maze[MazeIndex(x,y)];
}



 

/*function Update() {

    if (Input.GetKeyDown("r")) {

        WriteFile(filePath);

    }

    if (Input.GetKeyDown("f")) {

        ReadFile(filePath);

    }

}*/

 
function ResetFile(textFilePath:String)
{
	var sw : StreamWriter = new StreamWriter(textFilePath);
    sw.WriteLine("");

    sw.Flush();

    sw.Close();
}
/*function WriteFile(filepathIncludingFileName : String)

{

    sw = new StreamWriter.constructor(filepathIncludingFileName);

    sw.WriteLine("Line to write");

    sw.WriteLine("Another Line");

    sw.Flush();

    sw.Close();

}*/
/*function WriteType(textFilePath:String,type:int)
{
	sw = new StreamWriter(textFilePath);
	
	x = type.ToString();
	sw.Write(x+"-",true);
	sw.Flush();
	sw.Close();
}
function WritePosition(textFilePath:String,position:Vector3)
{
	sw = new StreamWriter(textFilePath);
	x = position.x.ToString();
	y = position.y.ToString();
	z = position.z.ToString();
	
	sw.Write(x+"-"+y+"-"+z+"-",true);
	sw.Flush();
	sw.Close();
}
function WriteRotation(textFilePath:String,rotation:Vector3)
{
	sw = new StreamWriter(textFilePath);
	rx = rotation.x.ToString();
	ry = rotation.y.ToString();
	rz = rotation.z.ToString();
	
	sw.Write(rx+"-"+ry+"-"+rz+"\n",true);
	sw.Flush();
	sw.Close();
}*/
function OpenStream(fileTextPath:String)
{
	sw = new StreamWriter(fileTextPath);
}
function CloseStream()
{
	sw.Flush();
	sw.Close();
}
function WriteObject(textFilePath:String, type:int,position:Vector3,rotation:Vector3)
{
	//sw = new StreamWriter(textFilePath);
	typeString = type.ToString();
	x = position.x.ToString();
	y = position.y.ToString();
	z = position.z.ToString();
	rx = rotation.x.ToString();
	ry = rotation.y.ToString();
	rz = rotation.z.ToString();
	
	sw.WriteLine(typeString+"|"+x+"|"+y+"|"+z+"|"+rx+"|"+ry+"|"+rz);
}

function ReadFile(filepathIncludingFileName : String) {

    sr = new File.OpenText(filepathIncludingFileName);

 

    input = "";

    while (true) {

        input = sr.ReadLine();

        if (input == null) 
        { break; }
		else
		{
			//var mySplitResult = input.Split("|");
			var mySplitResult = input.Split('|'[0]);
			
			var type:int =  parseInt(mySplitResult[0]);
			var x:int = parseInt(mySplitResult[1]);
			var y:int = parseInt(mySplitResult[2]);
			var z:int = parseInt(mySplitResult[3]);
			var rx:float = parseFloat(mySplitResult[4]);
			var ry:float = parseFloat(mySplitResult[5]);
			var rz:float = parseFloat(mySplitResult[6]);
			var position = new Vector3(x,y,z);
			var rotation = Quaternion.Euler(rx,ry,rz);
			
			if(type==1)
			{
				Instantiate(tile,position,rotation);
			}
			else if(type==2)
			{
				Instantiate(tile2,position,rotation);
			}
			else if(type==3)
			{
				Instantiate(tile3,position,rotation);
			}
			else if(type==4)
			{
				Instantiate(pickup,position,rotation);
			}
		}
        //Debug.Log("line="+input);

    }

    sr.Close();

}