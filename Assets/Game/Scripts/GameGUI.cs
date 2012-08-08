using UnityEngine;
using System.Collections;

public class GameGUI : MonoBehaviour {
	
	static public int score = 0;
	
	public GameObject upArrow;
	public GameObject downArrow;
	public GameObject leftArrow;
	public GameObject rightArrow;
	
	private GameObject firedObject;
	
	public float timeStart=0.0f;
	public bool startTimer=false;
	
	// Use this for initialization
	void Start () {
		upArrow.active=false;
		downArrow.active=false;
		leftArrow.active=false;
		rightArrow.active=false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("1"))
		{
			if (!startTimer){
				startTimer=true;
			}
			firedObject=upArrow;
		}
		if (Input.GetKeyDown("2"))
		{
			if (!startTimer){
				startTimer=true;
			}
			firedObject=downArrow;			
		}
		if (Input.GetKeyDown("3"))
		{
			if (!startTimer){
				startTimer=true;
			}
			firedObject=leftArrow;			
		}
		if (Input.GetKeyDown("4"))
		{
			if (!startTimer){
				startTimer=true;
			}
			firedObject=rightArrow;		
			
		}
		
		if (startTimer)
		{
			firedObject.active=true;
			timeStart+=Time.deltaTime;
			if (timeStart>2.0f)
			{
				startTimer=false;
				firedObject.active=false;
				timeStart=0.0f;
			}
		}
	}
	
	void OnGUI() {
		GUI.Label (new Rect (Screen.width - 100, 10, 100, 20), "Score: " + score);
	}
}