using UnityEngine;
using System.Collections;

public class SupportCamera : MonoBehaviour {
	
	public Camera supportCamera;
	
	// Use this for initialization
	void Start () {
		camera.transform.Translate (new Vector3(0, 0, -40));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
