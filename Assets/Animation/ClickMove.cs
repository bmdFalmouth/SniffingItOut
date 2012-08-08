using UnityEngine;
using System.Collections;

public class ClickMove : MonoBehaviour {
	public PlayerScript playerScript;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0))
		{
			Ray ray=Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (collider.Raycast(ray,out hit,100.0f))
			{
				playerScript.MoveToPoint(hit.point);
			}
		}
	}
}