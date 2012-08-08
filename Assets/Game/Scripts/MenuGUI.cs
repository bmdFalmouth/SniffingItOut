using UnityEngine;
using System.Collections;

public class MenuGUI : MonoBehaviour {
	
	public AudioClip[] clips;		
	AudioClip randomClip() {
		return clips[Random.Range (0, clips.GetLength (0) - 1)];
	}
	
	static public Texture2D playerAvatar;	
	static public string playerName;
	public Texture2D[] avatar = new Texture2D[8];
	public Texture2D tickImage;
	private string name = "";
	private int selectedAvatar = -1;
	
	// Use this for initialization
	void Start () {
		music.musicSource.volume += 1;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI() {
		GUI.color = Color.black;
		GUI.Label (new Rect(30, 20, 300, 20), "Please enter your name, and choose an avatar");
		name = GUI.TextArea (new Rect(30, 50, 150, 20), name);
		for (int i = 0; i < 8; i++) {
			GUI.color = Color.white;
			if (GUI.Button (new Rect(30 + 60 * i, 80, 50, 50), avatar[i])) {
				selectedAvatar = i;
				
				if (audio.isPlaying)
					audio.Stop ();
				this.gameObject.GetComponent<AudioSource>().clip = randomClip();
				audio.Play ();
			}
			if (i == selectedAvatar) {
				GUI.Label (new Rect(30 + 60 * i, 80, 30, 30), tickImage);
			}
		}
		
		if (name.Equals(string.Empty) || selectedAvatar < 0) {
			GUI.Box (new Rect(30, 150, 100, 50), "");
			GUI.Box (new Rect(150, 150, 100, 50), "");
		}
		else {
			GUI.color = Color.black;
			if (GUI.Button (new Rect(30, 150, 100, 50), "Client")) {
				if (audio.isPlaying)
					audio.Stop ();
				this.gameObject.GetComponent<AudioSource>().clip = randomClip();
				audio.Play ();
				
				playerAvatar = avatar[selectedAvatar];
				playerName = name;
				Application.LoadLevel ("RichSupportNetworkTest");
			}
			if (GUI.Button (new Rect(150, 150, 100, 50), "Server" )) {
				if (audio.isPlaying)
					audio.Stop ();
				this.gameObject.GetComponent<AudioSource>().clip = randomClip();
				audio.Play ();
				
				playerAvatar = avatar[selectedAvatar];
				playerName = name;
				Application.LoadLevel ("RichLeadNetworkTest");
			}
			
//			if (GUI.Button (new Rect(30, 150, 100, 50), "Continue")) {
//			playerAvatar = avatar[selectedAvatar];
//			playerName = name;
//			//start game
		}
		
		GUI.color = Color.black;
		if (GUI.Button (new Rect(270, 150, 100, 50), "Exit")) {
			if (audio.isPlaying)
					audio.Stop ();
				this.gameObject.GetComponent<AudioSource>().clip = randomClip();
				audio.Play ();
			
			Application.Quit();
		}
	}
}
