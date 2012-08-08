using UnityEngine;
using System.Collections;

public class DragAndDrop : MonoBehaviour {
	
	private GameObject selected=null;
	private bool beginDrag=false;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0) && selected==null)
		{
			beginDrag=true;
			Ray ray=Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (collider.Raycast(ray,out hit,100.0f))
			{
				selected=hit.collider.gameObject;
			}			
		}
		if (beginDrag)
		{
			Vector3 p = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x,Input.mousePosition.y,selected.transform.position.z));
			Vector3 newPos=selected.transform.position;
			newPos.x=p.x;
			newPos.y=p.y;
			if (Input.GetMouseButtonUp(0))
			{
				beginDrag=false;
				selected=null;
			}
		}
		
	}
}
