using UnityEngine;

using System.Collections;

public class Player : MonoBehaviour
{

	public string support2Vote = "";
	public float speed = 10.0f;
	public Transform spotLightTransform;
	public AudioClip stepOne;
	public AudioClip stepTwo;
	bool leftRight = false;
	public GameObject leftArrow;
	public GameObject rightArrow;
	public GameObject upArrow;
	public GameObject downArrow;
	public GameObject leftArrow2;
	public GameObject rightArrow2;
	public GameObject upArrow2;
	public GameObject downArrow2;
	public GameObject leftArrow3;
	public GameObject rightArrow3;
	public GameObject upArrow3;
	public GameObject downArrow3;
	public Vector3 x;
	bool sendLevel = false;
	int prefNetwork;


	public enum ArrowState
	{

		Up,

		Down,

		Left,

		Right,

		None=10,

	} ;

	public enum State
	{

		NO_SET,

		GAME,

		RATING,

		SCORES

	} ;


	public State state = State.NO_SET;
	State pState = State.NO_SET;
	public ArrowState arrowState1;
	public ArrowState arrowState2;
	public ArrowState arrowState3;
	int titleType = 0;
	Vector3 readPos;
	Vector3 readRot;
	int leadScore;
	int support1;
	int support2;
	int support3;
	public Transform wall;

	void Awake ()
	{

		DontDestroyOnLoad (this.gameObject);

		state = State.GAME;

		this.gameObject.GetComponent<NetworkView> ().observed = this.gameObject.GetComponent<Player> ();

	}

	void Start ()
	{

		spotLightTransform = GameObject.Find ("Spotlight").transform;

		arrowState1 = ArrowState.None;

		arrowState2 = ArrowState.None;

		arrowState3 = ArrowState.None;


		leftArrow = GameObject.Find ("Arrows/leftArrow1");

		rightArrow = GameObject.Find ("Arrows/rightArrow1");

		upArrow = GameObject.Find ("Arrows/upArrow1");

		downArrow = GameObject.Find ("Arrows/downArrow1");


		leftArrow2 = GameObject.Find ("Arrows/leftArrow2");

		rightArrow2 = GameObject.Find ("Arrows/rightArrow2");

		upArrow2 = GameObject.Find ("Arrows/upArrow2");

		downArrow2 = GameObject.Find ("Arrows/downArrow2");


		leftArrow3 = GameObject.Find ("Arrows/leftArrow3");

		rightArrow3 = GameObject.Find ("Arrows/rightArrow3");

		upArrow3 = GameObject.Find ("Arrows/upArrow3");

		downArrow3 = GameObject.Find ("Arrows/downArrow3");

	}

	void Update ()
	{


		switch (state) {

		case State.GAME:

			if (Network.isServer) {

				rigidbody.AddTorque (-speed * Input.GetAxis ("Horizontal") * Vector3.forward);

				rigidbody.AddTorque (speed * Input.GetAxis ("Vertical") * Vector3.right);


				if ((Input.GetAxis ("Horizontal") != 0 || Input.GetAxis ("Horizontal") != 0) && !this.gameObject.GetComponent<AudioSource> ().isPlaying) {

					if (leftRight)

						this.gameObject.GetComponent<AudioSource> ().clip = stepOne;
					else

						this.gameObject.GetComponent<AudioSource> ().clip = stepTwo;

					this.gameObject.GetComponent<AudioSource> ().Play ();

				}


				x = this.gameObject.transform.position;


				//leadScore = this.gameObject.GetComponent<ScoreTimer>();



			}


			if (Network.isClient) {

				this.gameObject.transform.position = x;

			}

			UpdateArrowStates ();

			break;


		case State.RATING:

			if (checkFirst ()) {

				Destroy (this.gameObject.GetComponent<Rigidbody> ());

				Destroy (this.gameObject.GetComponent<SphereCollider> ());

				Destroy (this.gameObject.GetComponent<MeshRenderer> ());

			} else {


			}

			break;

		}


		leadScore = (int)GameSettings.leadScore;

		support1 = (int)GameSettings.support1Score;

		support2 = (int)GameSettings.support2Score;

		support3 = (int)GameSettings.support3Score;




	}

	void HideAllArrows ()
	{

		leftArrow.active = false;

		rightArrow.active = false;

		upArrow.active = false;

		downArrow.active = false;

	}

	void HideAllArrows2 ()
	{

		leftArrow2.active = false;

		rightArrow2.active = false;

		upArrow2.active = false;

		downArrow2.active = false;

	}

	void HideAllArrows3 ()
	{

		leftArrow3.active = false;

		rightArrow3.active = false;

		upArrow3.active = false;

		downArrow3.active = false;

	}

	void UpdateArrowStates ()
	{

		switch (arrowState1) {

		case ArrowState.None:

			{

				HideAllArrows ();

				break;

			}


		case ArrowState.Left:

			{

				HideAllArrows ();

				leftArrow.active = true;

				break;

			}


		case ArrowState.Right:

			{

				HideAllArrows ();

				rightArrow.active = true;

				break;

			}


		case ArrowState.Up:

			{

				HideAllArrows ();

				upArrow.active = true;

				break;

			}


		case ArrowState.Down:

			{

				HideAllArrows ();

				downArrow.active = true;

				break;

			}

		}


		switch (arrowState2) {

		case ArrowState.None:

			{

				HideAllArrows2 ();

				break;

			}


		case ArrowState.Left:

			{

				HideAllArrows2 ();

				leftArrow2.active = true;

				break;

			}


		case ArrowState.Right:

			{

				HideAllArrows2 ();

				rightArrow2.active = true;

				break;

			}


		case ArrowState.Up:

			{

				HideAllArrows2 ();

				upArrow2.active = true;

				break;

			}


		case ArrowState.Down:

			{

				HideAllArrows2 ();

				downArrow2.active = true;

				break;

			}

		}



		switch (arrowState3) {

		case ArrowState.None:

			{

				HideAllArrows3 ();

				break;

			}


		case ArrowState.Left:

			{

				HideAllArrows3 ();

				leftArrow3.active = true;

				break;

			}


		case ArrowState.Right:

			{

				HideAllArrows3 ();

				rightArrow3.active = true;

				break;

			}


		case ArrowState.Up:

			{

				HideAllArrows3 ();

				upArrow3.active = true;

				break;

			}


		case ArrowState.Down:

			{

				HideAllArrows3 ();

				downArrow3.active = true;

				break;

			}

		}



		if (spotLightTransform != null) {

			Vector3 spotPosUpdate = spotLightTransform.position;

			spotPosUpdate.x = transform.position.x;

			spotPosUpdate.z = transform.position.z;

			spotLightTransform.position = spotPosUpdate;

		}


	}

	void OnGUI ()
	{

		//GUI.Label (new Rect (500, 500, 100, 100), leadScore);

		//GUI.Label (new Rect (500, 700, 100, 100), GameSettings.leadScore);

	}

	bool checkFirst ()
	{

		if (state != pState) {

			pState = state;

			return true;

		} else

			return false;

	}

	public void SerLevel ()
	{


		sendLevel = true;

	}

	void OnSerializeNetworkView (BitStream stream, NetworkMessageInfo info)
	{




		if (stream.isWriting) {

			stream.Serialize (ref x);

			if (Network.isServer) {

				stream.Serialize (ref leadScore);

				stream.Serialize (ref support1);

				stream.Serialize (ref support2);

				stream.Serialize (ref support3);

			}


/*if(sendLevel && Network.isServer)

{

titleType = 1; 

foreach(GameObject piece in GameObject.FindGameObjectsWithTag("MazeWall"))

{

readPos = piece.transform.position;

readRot = piece.transform.rotation.eulerAngles;


stream.Serialize(ref titleType); 

stream.Serialize(ref readPos); 

stream.Serialize(ref readRot); 

}


sendLevel = false;

} */



		} else {

			stream.Serialize (ref x);


			stream.Serialize (ref x);


			if (Network.isClient) {

				stream.Serialize (ref leadScore);

				stream.Serialize (ref support1);

				stream.Serialize (ref support2);

				stream.Serialize (ref support3); 


				GameSettings.leadScore = leadScore;

				GameSettings.support1Score = support1;

				GameSettings.support2Score = support2;

				GameSettings.support3Score = support3;

			}


/*switch(titleType)

{

case 1:

Instantiate(wall,readPos, Quaternion.Euler(readRot));

break;

} */



		}

	}

}