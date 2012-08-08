#pragma strict
var Template : UnityEngine.Transform; 
var Support : UnityEngine.Transform; 
var playerIndex : int;

function Start () 
{

}

function Update () 
{

}

function OnNetworkLoadedLevel()
{
	if(Network.connections.Length == 0)
		Network.Instantiate(Template, transform.position, Quaternion.AngleAxis(180.0,Vector3.up),0);
	else
	{
		if(GameObject.FindGameObjectsWithTag("Support").Length < Network.connections.Length)
		{
			Debug.Log("Number of connections: " + Network.connections.Length.ToString());
			Network.Instantiate(Support, transform.position, transform.rotation,0);
			GameObject.FindGameObjectWithTag("Player").rigidbody.active = false;
		}
	}
}

function OnPlayerDisconnected(player : NetworkPlayer)
{
	Network.RemoveRPCs(player,0);
	Network.DestroyPlayerObjects(player);
}