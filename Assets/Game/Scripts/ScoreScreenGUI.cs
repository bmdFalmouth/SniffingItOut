using UnityEngine;
using System.Collections;

public class ScoreScreenGUI : MonoBehaviour {
	
	public AudioClip[] clips;		
	AudioClip randomClip() {
		return clips[Random.Range (0, clips.GetLength (0) - 1)];
	}
	
	public Texture2D avatar1;
	public Texture2D avatar2;
	public Texture2D avatar3;
	public Texture2D avatar4;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI() {
		GUI.color = Color.white;
		GUI.Label (new Rect(30, 30, 64, 64), MenuGUI.playerAvatar);
		GUI.Label (new Rect(30, 124, 64, 64), avatar2);
		GUI.Label (new Rect(30, 218, 64, 64), avatar3);
		GUI.Label (new Rect(30, 312, 64, 64), avatar4);
		
		GUI.color = Color.black;
		GUI.Label (new Rect(35, 10, 50, 20), MenuGUI.playerName);
		GUI.Label (new Rect(100, 42, 100, 20), "Score: " + GameGUI.score);
		GUI.Label (new Rect(100, 136, 100, 20), "Score: ");
		GUI.Label (new Rect(100, 230, 100, 20), "Score: ");
		GUI.Label (new Rect(100, 324, 100, 20), "Score: ");
		
		if (GUI.Button (new Rect(Screen.width - 320, Screen.height - 50, 100, 50), "Continue")) {
			if (audio.isPlaying)
				audio.Stop ();
			this.gameObject.GetComponent<AudioSource>().clip = randomClip();
			audio.Play ();
			
			Application.LoadLevel ("RichLeadNetworkTest");
		}
		
		if (GUI.Button (new Rect(Screen.width - 210, Screen.height - 50, 100, 50), "Main Menu")) {
			if (audio.isPlaying)
				audio.Stop ();
			this.gameObject.GetComponent<AudioSource>().clip = randomClip();
			audio.Play ();
			
			Application.LoadLevel ("MainMenu");
		}
		
		if (GUI.Button (new Rect(Screen.width - 100, Screen.height - 50, 100, 50), "Exit")) {
			if (audio.isPlaying)
				audio.Stop ();
			this.gameObject.GetComponent<AudioSource>().clip = randomClip();
			audio.Play ();
			
			Application.Quit ();
		}
	}
}
