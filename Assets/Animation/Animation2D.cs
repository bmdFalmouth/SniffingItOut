using UnityEngine;
using System.Collections;

public class Animation2D : MonoBehaviour {

	public int columnCount = 24; //Here you can place the number of columns of your sheet.
                           //The above sheet has 24
    public int rowCount = 1; //Here you can place the number of rows of your sheet.
                          //The above sheet has 1
    public float framesPerSecond = 10.0f;

	public bool start=false;
	public bool flip=false;
	
	public int currentAnimationRow=0;
	public int currentAnimationColumn=0;
	
	private int currentAnimationIndex=0;
	
	public void StartAnimation()
	{
		start=true;
	}
	
	public void StopAnimation()
	{
		start=false;
		currentAnimationIndex=0;
	}
	
	// Use this for initialization
	void Start () {
		SetSpriteAnimation(columnCount,rowCount,currentAnimationRow,currentAnimationColumn,framesPerSecond);
	}
	
	void SetSpriteAnimation(int rowCount,int columnCount,int rowNumber,int columnNumber,float fps)
	{
		//int index=currentAnimationIndex;
		if (start){
			currentAnimationIndex=(int)(Time.time*fps);
			currentAnimationIndex=currentAnimationIndex%columnCount;
			//currentAnimationIndex=index;
		}
		
		Vector2 size=new Vector2(1.0f/(float)columnCount,1.0f/(float)rowCount);
		
		int uIndex=currentAnimationIndex%columnCount;
		int vIndex=currentAnimationIndex/columnCount;
		
		Vector2 offset=new Vector2((uIndex+columnNumber)*size.x,(1.0f-size.y)-(vIndex+rowNumber)*size.y);
		if (flip){
			offset.y*=-1;
			size.y*=-1;
		}
    	renderer.material.SetTextureOffset ("_MainTex", offset);
    	renderer.material.SetTextureScale ("_MainTex", size);		
	}
	
	// Update is called once per frame
	void Update () {
		SetSpriteAnimation(rowCount,columnCount,currentAnimationRow,currentAnimationColumn,framesPerSecond);
		/*
    	// Calculate index
    	int index = (int)(Time.time * framesPerSecond);
    	// repeat when exhausting all frames
    	index = (index % (int)(uvAnimationTileX * uvAnimationTileY));
   
    	// Size of every tile
    	Vector2 size = new Vector2 (1.0f / (float)uvAnimationTileX, 1.0f / (float)uvAnimationTileY);
   
    	// split into horizontal and vertical index
    	float uIndex = (index % uvAnimationTileX);
    	float vIndex = (index / uvAnimationTileX)*currentAnimationRow;

    	// build offset
    	// v coordinate is the bottom of the image in opengl so we need to invert.
   	 	Vector2 offset =new  Vector2 ((float)(uIndex * size.x), (float)(1.0f - size.y - vIndex * size.y));
   
    	renderer.material.SetTextureOffset ("_MainTex", offset);
    	renderer.material.SetTextureScale ("_MainTex", size);	*/
	}
}
