using UnityEngine;
using System.Collections;

public class Mound : MonoBehaviour {
	
	public Transform pickup;
	private bool pickupSpawn=false;
	public Transform chest;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag.Equals("Player")) {
			
			
			this.gameObject.GetComponent<iTweenEvent>().Play();
			if(!pickupSpawn)
			{
				Instantiate(pickup,new Vector3(this.transform.position.x+1,this.transform.position.y,this.transform.position.z+1),Quaternion.identity);
				pickupSpawn = true;
			}
			Destroy(gameObject,0.5f);
			
			audio.Play ();
		}
	}
}
