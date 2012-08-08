using UnityEngine;
using System.Collections;

public class IntroGUI : MonoBehaviour {
	
	public AudioClip[] clips;		
	AudioClip randomClip() {
		return clips[Random.Range (0, clips.GetLength (0) - 1)];
	}
	
	public Texture2D logo;
	public Texture2D titleCard;
	public AudioClip sniffing;
	public Texture2D story;
	public Texture2D panel1;
	public AudioClip ping;
	public Texture2D panel2;
	public AudioClip cheer;
	public Texture2D panel3;
	public AudioClip ooo;
	public Texture2D panel4;
	public Texture2D panel5;
	public AudioClip evilLaugh;
	public Texture2D panel6;
	public AudioClip yay;
	private float startTime = 0.0f;
	private float currentTime = 0.0f;
	private const int timeOut = 3;
	bool audioPlayed = false;
	
	public GUIStyle style;
	enum State {
		
		logo,
		titleScreen,
		storyScreen,
		panel1,
		panel2,
		panel3,
		panel4,
		panel5,
		panel6,
		tutorialScreen2
	};
	
	private State introState = State.logo;
	
	// Use this for initialization
	void Start () {
		Random.seed = (int)Time.deltaTime;
	}
	
	// Update is called once per frame
	void Update () {
		
		currentTime += Time.deltaTime;
		
		switch (introState) {
		
		case State.logo:
			if (Input.GetKeyDown ("space")) {
				introState = State.titleScreen;
			}
			break;
			
		case State.titleScreen:
			if (Input.GetKeyDown ("space")) {
				introState = State.storyScreen;
				audioPlayed = false;
				music.musicSource.volume -= 3;
			}
			break;
			
		case State.panel1:
			if (currentTime - startTime > timeOut) {
				introState = State.panel2;
				startTime = 0.0f;
				currentTime = 0.0f;
				audioPlayed = false;
			}
			break;
			
		case State.panel2:
			if (currentTime - startTime > timeOut) {
				introState = State.panel3;
				startTime = 0.0f;
				currentTime = 0.0f;
				audioPlayed = false;
			}
			break;
			
		case State.panel3:
			if (currentTime - startTime > timeOut) {
				 introState = State.panel4;
				startTime = 0.0f;
				currentTime = 0.0f;
				audioPlayed = false;
			}
			break;
			
		case State.panel4:
			if (currentTime - startTime > timeOut) {
				introState = State.panel5;
				startTime = 0.0f;
				currentTime = 0.0f;
				audioPlayed = false;
			}
			break;
			
		case State.panel5:
			if (currentTime - startTime > timeOut) {
				introState = State.panel6;
				startTime = 0.0f;
				currentTime = 0.0f;
				audioPlayed = false;
			}
			break;
			
		case State.panel6:
			if (currentTime - startTime > timeOut) {
				introState = State.tutorialScreen2;
				startTime = 0.0f;
				currentTime = 0.0f;
				audioPlayed = false;
			}
			break;
		}
	}
	
	void OnGUI() {
		if (introState == State.logo) {
			GUI.DrawTexture (new Rect(Screen.width / 2 - 379, Screen.height / 2 - 256, 758, 512), logo);
			GUI.color = Color.black;
			GUI.Label (new Rect(Screen.width / 2 - 50, Screen.height - 20, 150, 20), "Press space to continue.");
		}
		
		else if (introState == State.titleScreen) {
			if (!audioPlayed) {
				this.gameObject.GetComponent<AudioSource>().clip = sniffing;
				audio.Play ();
				audioPlayed = true;
			}
			
			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), titleCard, ScaleMode.StretchToFill);
			GUI.color = Color.white;
			GUI.Label (new Rect(Screen.width / 2 - 50, Screen.height - 20, 150, 20), "Press space to continue.");
		}
		
		else if (introState == State.storyScreen) {
			GUI.color = Color.black;
			GUI.Label (new Rect(Screen.width / 2 - 200, 0, 400, 400), story);
			if (GUI.Button (new Rect(Screen.width / 2 - 110, 350, 100, 50), "How to play")) {
				if (!audioPlayed) {
					audio.Stop ();
					this.gameObject.GetComponent<AudioSource>().clip = randomClip();
					audio.Play ();
					audioPlayed = true;
				}
				
				startTime = 0.0f;
				currentTime = 0.0f;
				introState = State.panel1;
			}
			if (GUI.Button (new Rect(Screen.width / 2 + 110, 350, 100, 50), "Sniff it Out")) {
				music.musicSource.volume += 2;
				
				if (!audioPlayed) {
					audio.Stop ();
					this.gameObject.GetComponent<AudioSource>().clip = randomClip();
					audio.Play ();
					audioPlayed = true;
				}
				
				Application.LoadLevel ("MainMenu");
			}
		}
		
		else if (introState == State.panel1) {
			
			if (!audioPlayed) {
				audio.Stop ();
				this.gameObject.GetComponent<AudioSource>().clip = ping;
				audio.Play ();
				audioPlayed = true;
			}
			
			GUI.DrawTexture (new Rect(0, 0, Screen.width, Screen.height), panel1, ScaleMode.StretchToFill);
		}
		
		else if (introState == State.panel2) {
			if (!audioPlayed) {
				audio.Stop ();
				this.gameObject.GetComponent<AudioSource>().clip = cheer;
				audio.Play ();
				audioPlayed = true;
			}
			
			GUI.DrawTexture (new Rect(0, 0, Screen.width, Screen.height), panel2, ScaleMode.StretchToFill);
		}
		
		else if (introState == State.panel3) {
			if (!audioPlayed) {
				audio.Stop ();
				this.gameObject.GetComponent<AudioSource>().clip = ooo;
				audio.Play ();
				audioPlayed = true;
			}
			
			GUI.DrawTexture (new Rect(0, 0, Screen.width, Screen.height), panel3, ScaleMode.StretchToFill);
		}
		
		else if (introState == State.panel4) {
			if (!audioPlayed) {
				audio.Stop ();
				this.gameObject.GetComponent<AudioSource>().clip = sniffing;
				audio.Play ();
				audioPlayed = true;
			}
			
			GUI.DrawTexture (new Rect(0, 0, Screen.width, Screen.height), panel4, ScaleMode.StretchToFill);
		}
		
		else if (introState == State.panel5) {
			if (!audioPlayed) {
				audio.Stop ();
				this.gameObject.GetComponent<AudioSource>().clip = evilLaugh;
				audio.Play ();
				audioPlayed = true;
			}
			
			GUI.DrawTexture (new Rect(0, 0, Screen.width, Screen.height), panel5, ScaleMode.StretchToFill);
		}
		
		else if (introState == State.panel6) {
			if (!audioPlayed) {
				audio.Stop ();
				this.gameObject.GetComponent<AudioSource>().clip = yay;
				audio.Play ();
				audioPlayed = true;
			}
			
			GUI.DrawTexture (new Rect(0, 0, Screen.width, Screen.height), panel6, ScaleMode.StretchToFill);
		}
		
		else if (introState == State.tutorialScreen2) {
			music.musicSource.volume += 2;
			
			GUI.Label (new Rect(Screen.width / 2 - 96, 30, 250, 20), "You control patch with the arrow keys",style);
			GUI.Label (new Rect(Screen.width / 2 - 89, 60, 250, 20), "Your \"friends\" direct you with theirs",style);
			GUI.Label (new Rect(Screen.width / 2 - 155, 120, 350, 20), "If anyone leaves the game, their score is shared with other players",style);
			GUI.Label (new Rect(Screen.width / 2 - 19, 180, 150, 20), "Time is limited",style);
			GUI.Label (new Rect(Screen.width / 2 - 10, 210, 100, 20), "Good luck",style);
			
			if (GUI.Button (new Rect(Screen.width / 2 - 50, Screen.height - 60, 100, 50), "Continue")) {
				if (!audioPlayed) {
					audio.Stop ();
					this.gameObject.GetComponent<AudioSource>().clip = randomClip();
					audio.Play ();
				}
				
				Application.LoadLevel ("MainMenu");
			}
		}
	}
}