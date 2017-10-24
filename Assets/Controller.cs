using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {	//TODO: Inherit Body class?? e.g code clean up

	public float speed = 2.0f;
	public float jump = 200.0f;

	public Collider2D cdr;
	public Rigidbody2D rb;

	// For Jump Raycast
	public bool onGround = false;
	public float yOffset = 0.1f; // Offset for player sprite
	private int LayerGround;
	public Vector2 groundStart;	// For jump raycast
	public Vector2 groundEnd;	// For jump raycast
	public float groundEndDist = 0.1f;	// For jump raycast

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> (); //assign rigidbody to 2d
		cdr = GetComponent<Collider2D> (); //assign collider to 2d
		LayerGround = LayerMask.NameToLayer ("Ground");	//gets layer "Ground" from unity

	}
	
	// Update is called once per frame
	void Update () {



	}

	// Use for physics stuff
	void FixedUpdate () {
		playerMovement ();

	}


	//*******************Helper Functions******************//

	// Player Stuffs
	void playerMovement() {
		// Left
		if (Input.GetKey (KeyCode.A)) {
			transform.position += Vector3.left * speed * Time.deltaTime;
		}

		// Right
		if (Input.GetKey (KeyCode.D)) {
			transform.position += Vector3.right * speed * Time.deltaTime;
		}

		playerJump ();
	}

	void playerJump() {
		// Jump
		if (Input.GetKeyDown (KeyCode.W))  {

			// Set/update Raycast line
			groundStart = transform.position;
			groundStart.y -= yOffset;
			groundEnd = transform.position;
			groundEnd.y -= groundEndDist;

			//playerJumpDEBUG();
			RaycastHit2D groundHit = Physics2D.Linecast(groundStart, groundEnd);

			//Debug.Log ("GROUNDHIT: " + groundHit.transform.gameObject.layer);
			//Debug.Log ("LAYERGROUND: " + LayerGround);


			if (Physics2D.Linecast (groundStart, groundEnd)) {
				if (groundHit.transform.gameObject.layer == LayerGround) {
					onGround = true;
					rb.AddForce (Vector2.up * jump); // Add impulse
					Debug.Log ("JUMP");
				}
			}

			onGround = false;
			//Debug.Log ("GROUNDED NOOOOOOO");
		}
	}

	void playerJumpDEBUG() {
		// Debug lines
		groundStart = transform.position;
		groundStart.y -= yOffset;


		//groundEnd.y -= playerOffset;
		groundEnd = transform.position;
		groundEnd.y -= groundEndDist;

		Debug.DrawLine(groundStart, groundEnd, Color.green);
	}

}
