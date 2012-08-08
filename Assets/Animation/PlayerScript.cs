using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
	
	Vector3 endPosition;
	bool move=false;
	public float speed=1.0f;
	public Animation2D animationController;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (move)
		{
			//get the directional vector
			Vector3 dir=endPosition-transform.position;
			dir.Normalize();
			transform.position+=dir*speed*Time.deltaTime;
			if (collider.bounds.Contains(endPosition))
			{
				move=false;
				animationController.StopAnimation();
			}
			//if (endPosition==transform.position)
			//{
			//	move=false;
			//}
			Debug.DrawLine (transform.position, endPosition, Color.red);
		}
	}
	
	public void MoveToPoint(Vector3 endPoint)
	{
		animationController.StartAnimation();
		move=true;
		endPosition=endPoint;
	}
	
	
}
