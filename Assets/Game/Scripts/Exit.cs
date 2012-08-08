using UnityEngine;
using System.Collections;

public class Exit : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider other) 
	{
		if (other.tag.Equals("Player")) 
		{
			if(Network.isServer)
			{
				foreach(NetworkPlayer player in Network.connections)
				{
					Network.CloseConnection(player, true);
				}
			}
			//Network.CloseConnection(
			other.GetComponent<Player>().state = Player.State.RATING;
			Application.LoadLevel("VoteScreen");
			Debug.Log ("Exit");
		}
	}
}
