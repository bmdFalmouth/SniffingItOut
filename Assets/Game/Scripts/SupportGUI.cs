using UnityEngine;
using System.Collections;

public class SupportGUI : MonoBehaviour {
	
	public string arrow;
	public Texture2D upArrow;
	public Texture2D downArrow;
	public Texture2D leftArrow;
	public Texture2D rightArrow;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("up")) {
			
		}
		if (Input.GetKeyDown ("dowm")) {
			
		}
		if (Input.GetKeyDown("left")) {
			
		}
		if (Input.GetKeyDown ("right")) {
			
		}
	}
	
	void OnGUI() {
		if (GUI.Button(new Rect(Screen.width / 2 - 138, Screen.height - 64, 64, 64), upArrow)) {
			arrow = "up";
			Debug.Log("up");
		}
		
		if (GUI.Button(new Rect(Screen.width / 2 - 64, Screen.height - 64, 64, 64), downArrow)) {
			arrow = "down";
			Debug.Log("down");
		}
		
		if (GUI.Button(new Rect(Screen.width / 2 + 10, Screen.height - 64, 64, 64), leftArrow)) {
			arrow = "left";
			Debug.Log("left");
		}
		
		if (GUI.Button(new Rect(Screen.width / 2 + 84, Screen.height - 64, 64, 64), rightArrow)) {
			arrow = "right";
			Debug.Log("right");
		}
	}
}
