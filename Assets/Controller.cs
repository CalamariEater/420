using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {	//TODO: Inherit Body class?? e.g code clean up

	public float speed = 2.0f;
	public float jump = 200.0f;
	public int hp = 10;
	public int jumps = 0;
	public int jumpLimit = 2;

	public Collider2D cdr;
	public Rigidbody2D rb;

	// For Jump Raycast
	public bool onGround = false;
	public float yOffset = 0.1f; // Offset for player sprite ~ not dynamic
	private int LayerGround;
	public Vector2 groundStart;	// For jump raycast
	public Vector2 groundEnd;	// For jump raycast
	public float groundEndDist = 0.1f;	// For jump raycast

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> (); //assign rigidbody to 2d
		cdr = GetComponent<Collider2D> (); //assign collider to 2d
		LayerGround = LayerMask.NameToLayer ("Ground");	//gets layer "Ground" from unity
		rb.freezeRotation = true;

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
		if (Input.GetKeyDown (KeyCode.W) || Input.GetKeyDown(KeyCode.Space))  {

			// Set/update Raycast line
			groundStart = transform.position;
			groundStart.y -= yOffset;
			groundEnd = transform.position;
			groundEnd.y -= groundEndDist;

			//playerJumpDEBUG();
			RaycastHit2D groundHit = Physics2D.Linecast(groundStart, groundEnd);

			//Debug.Log ("GROUNDHIT: " + groundHit.transform.gameObject.layer);
			//Debug.Log ("LAYERGROUND: " + LayerGround);


//			if (groundHit) {
//				if (groundHit.transform.gameObject.layer == LayerGround) {
//					rb.AddForce (Vector2.up * jump); // Add impulse
//					onGround = true;
//					jumps++;
//					//Debug.Log ("JUMP");
//				}
//			} else if (jumps < jumpLimit) {
//				rb.AddForce (Vector2.up * jump); // Add impulse
//				onGround = true;
//				jumps++;
//				Debug.Log ("DOOOUBLE JUMP");
//			} else if (!groundHit) {
//				jumps = 0;
//				onGround = false;
//				//Debug.Log ("GROUNDED NOOOOOOO");
//			}

			if (groundHit) {
				if (groundHit.transform.gameObject.layer == LayerGround) {
					onGround = true;
					jumps = 0;
				}
			} 
			if (jumps < jumpLimit) {
				Vector2 v = rb.velocity;
				v.y = 0.0f;	
				rb.velocity = v; // Set Y velocity to 0 ~ avoids spam jump high af bug
				rb.AddForce (Vector2.up * jump); // Add impulse
				onGround = false;	// TODO: Small bug where jumping doesnt register
				jumps++;
				Debug.Log ("DOOOUBLE JUMP");
			} 
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
