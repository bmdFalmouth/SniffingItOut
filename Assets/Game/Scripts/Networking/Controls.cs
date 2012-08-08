using UnityEngine;
using System.Collections;

public class Controls : MonoBehaviour 
{
	public int x = -1;

	
	private Player player;
	// Use this for initialization
	void Start () 
	{
		this.gameObject.GetComponent<NetworkView>().observed = this.gameObject.GetComponent<Controls>();	
		
		
	}
	
	void Awake()
	{
		DontDestroyOnLoad(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	void OnGUI()
	{
		if(Network.isClient && Application.loadedLevelName == "RichSupportNetworkTest")
		{			
			if(GUI.Button(new Rect(20,110,50,50), "up"))
			{
				x = 0;
			}
				
			if(GUI.Button(new Rect(20,160,50,50), "down"))
			{				
				x = 1;
			}
			
			if(GUI.Button(new Rect(20,260,50,50), "right"))
			{
				x = 3;
			}
			
			
			if(GUI.Button(new Rect(20,210,50,50), "left"))
			{
				x = 2;
			}
			
			GUI.Label(new Rect(100,500,200,200),Network.player.ToString());
		}
	}
	

	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
	{
		
		float LeadScore = GameSettings.leadScore;
		float Support1Score = GameSettings.support1Score;
		float Support2Score = GameSettings.support2Score;
		float Support3Score = GameSettings.support3Score;
		
		if(stream.isWriting)
		{
			stream.Serialize(ref x);
			stream.Serialize(ref LeadScore);
			stream.Serialize(ref Support1Score);
			stream.Serialize(ref Support2Score);
			stream.Serialize(ref Support3Score);	
		}
		else
		{
			stream.Serialize(ref x);
			
				
			switch(info.sender.ToString())
			{
				case "0":
				if(Network.isClient && Application.loadedLevelName == "ScoreScreen")
				{
					stream.Serialize(ref LeadScore);
					stream.Serialize(ref Support1Score);
					stream.Serialize(ref Support2Score);
					stream.Serialize(ref Support3Score);	
				}
				
				break;
				case "1":
				if (x<10){
				player=GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
				player.arrowState1 = (Player.ArrowState)x;}
				break;
				case "2":
				if (x<10){
				player=GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
				player.arrowState2 = (Player.ArrowState)x;}
				break;
				case "3":
				if (x<10){
				player=GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
				player.arrowState3 = (Player.ArrowState)x;}
				break;
			}
			
		}
	}
	
	 void OnDisconnectedFromServer(NetworkDisconnection info) 
	{
        if (Network.isClient)
            Application.LoadLevel("ScoreScreen");
    }
}