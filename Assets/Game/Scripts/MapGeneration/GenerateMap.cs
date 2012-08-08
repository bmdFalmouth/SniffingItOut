using UnityEngine;
using System.Collections;

public class GenerateMap : MonoBehaviour {
	
	public GameObject tilePrefab;
	
	public int mapX=128;
	public int mapY=128;
	
	public int tileWidth=50;
	public int tileHeight=50;
	
	public int gap=5;
	
	// Use this for initialization
	void Start () {
		Vector3 pos=transform.position;
		
		for (int x=0;x<mapX;x++)
		{
			for (int y=0;y<mapY;y++)
			{
				Instantiate(tilePrefab,pos,Quaternion.identity);
				pos.z+=tileHeight;
			}
			
			pos.x+=tileWidth;
			pos.z=transform.position.z;
		}
		
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
