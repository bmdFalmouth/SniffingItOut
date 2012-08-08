#pragma strict
var remoteIP = "10.39.70.175";
var remotePort = 25000;
var listenPort = 25000;
var useNAT = false;
var yourIP = "";
var yourPort = "";

function Awake()
{
	DontDestroyOnLoad(this.gameObject);
}

function Start () 
{

}

function Update () 
{

}

function OnGUI()
{
	if(Network.peerType == NetworkPeerType.Disconnected)
	{
		if(GUI.Button (new Rect(10,10,100,30),"Connect"))
		{
			Network.useNat = useNAT;
			Network.Connect(remoteIP, remotePort);
		}
		
		if(GUI.Button(new Rect(10,50,100,30),"Start Server"))
		{
			Network.useNat = useNAT;
			Network.InitializeServer(4,listenPort);
			
			for (var go : GameObject in FindObjectsOfType(GameObject))
  			 {
  			 	go.SendMessage("OnNetworkLoadedLevel", SendMessageOptions.DontRequireReceiver); 
  			 }
 
		}	 			 
  	remoteIP = GUI.TextField(new Rect(120,10,1200,20), remoteIP);
  	remotePort = parseInt(GUI.TextField(new Rect(230,10,40,20),remotePort.ToString()));
	} 
	else
	{
		var ipaddress = Network.player.ipAddress;
		var port = Network.player.port.ToString();
		
		 GUI.Label(new Rect(140,20,250,40),"IP Adress: "+ipaddress+":"+port);
  	     if (GUI.Button (new Rect(10,10,100,50),"Disconnect"))
         {
   			Network.Disconnect(200);
  		 }
	}
} 

function OnConnectedToServer()
{
	 /*Debug.Log ("I HAVE DONE THIS LOG!");
	    
	    var player:Player;
	    var playerObject:GameObject = GameObject.FindGameObjectWithTag("Player");
	    player = playerObject.GetComponent("Player");
	 	player.SerLevel();*/
	 	
    for (var go : GameObject in FindObjectsOfType(GameObject))
	    go.SendMessage("OnNetworkLoadedLevel", SendMessageOptions.DontRequireReceiver);
	    
	   
	   
	   
}