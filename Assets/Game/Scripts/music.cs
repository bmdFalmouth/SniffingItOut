using UnityEngine;
using System.Collections;

public class music : MonoBehaviour {
	
	static public AudioSource musicSource;
	public float playRate= 2;
	public float nextPlayTime;

	// Use this for initialization
	void Start () {
		if (this.gameObject.GetComponent<AudioSource>() != null) {
			musicSource = this.gameObject.GetComponent<AudioSource>();
			nextPlayTime = Time.realtimeSinceStartup + playRate;
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.realtimeSinceStartup > nextPlayTime) {
			musicSource.Play ();
			nextPlayTime = Time.realtimeSinceStartup + playRate + musicSource.clip.length;
		}
	
	}
	
	void Awake() {
		DontDestroyOnLoad(this.gameObject);
	}
}
