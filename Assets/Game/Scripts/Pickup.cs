using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour {
	
	private float timer;
	enum PickUpType {Bling,Dinosaur,MusicBox,Shark,Skull};
	
	public AudioClip soundEffect;
	public int scoreValue = 5;
	
	PickUpType type;
	// Use this for initialization
	void Start () {
		
		timer =0;
		type = (PickUpType)UnityEngine.Random.Range (0,5);
		
		switch (type)
		{
		case PickUpType.Bling:
			this.gameObject.renderer.material.mainTexture = (Texture)Resources.Load("Objects/Cupcake");
			this.scoreValue = 5;
			break;
		case PickUpType.Dinosaur:
			this.gameObject.renderer.material.mainTexture = (Texture)Resources.Load("Objects/Dinosaur");
			this.scoreValue = 5;
			break;
		case PickUpType.MusicBox:
			this.gameObject.renderer.material.mainTexture = (Texture)Resources.Load("Objects/Painting-02");
			this.scoreValue = 5;
			break;
		case PickUpType.Shark:
			this.gameObject.renderer.material.mainTexture = (Texture)Resources.Load("Objects/Shark");
			this.scoreValue = 5;
			break;
		case PickUpType.Skull:
			this.gameObject.renderer.material.mainTexture = (Texture)Resources.Load("Objects/Cupcake");
			this.scoreValue = 5;
			break;
				
		}
	}
	
	// Update is called once per frame
	void Update () {
		timer+=Time.deltaTime;
	
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.tag.Equals("Player")) {
			
			if(timer>2)
			{
			this.gameObject.GetComponent<iTweenEvent>().Play();
			Destroy(gameObject,0.5f);
			
			audio.Play ();
			
			//GameGUI gameGUI = Camera.main.GetComponent<GameGUI>();
				//GameGUI.score += scoreValue;
			}
		}
	}
	void OnTriggerStay(Collider other) {
		if (other.tag.Equals("Player")) {
			
			if(timer>2)
			{
			this.gameObject.GetComponent<iTweenEvent>().Play();
			Destroy(gameObject,0.5f);
			
			audio.Play ();
			
			//GameGUI gameGUI = Camera.main.GetComponent<GameGUI>();
				//GameGUI.score += scoreValue;
			}
		}
	}
}
