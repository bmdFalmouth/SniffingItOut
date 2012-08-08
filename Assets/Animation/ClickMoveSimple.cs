using UnityEngine;
using System.Collections;

public class ClickMoveSimple : MonoBehaviour {
	public GameObject playerObject;
	
	
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
				playerObject.transform.position=hit.point;
				//logic goes here
			}
		}
	}
}
