using UnityEngine;
using System.Collections;

public class BaddieKeepAway: MonoBehaviour {	//TODO: Inherit Body class?? e.g code clean up

	public float speed = 2.0f;
	public float jump = 200.0f;

	public Collider2D cdr;
	public Rigidbody2D rb;

	// For Jump Raycast
	private int LayerGround;
	public float yOffset = 0.1f; // Offset for baddie sprite
	public bool onGround = false;
	public Vector2 groundStart;	// For jump raycast
	public Vector2 groundEnd;	// For jump raycast
	public float groundEndDist = 0.1f;	// For jump raycast

	// For Keep Away Raycast
	private int LayerPlayer;
	public float xOffset = 1.0f; // Offset for baddie sprite ~ distance TODO: change these var names its confusing
	public bool tooClose = false;
	public Vector2 keepAwayStart;
	public Vector2 keepAwayEnd;
	public float keepAwayEndDist = 0.1f;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> (); //assign rigidbody to 2d
		cdr = GetComponent<Collider2D> (); //assign collider to 2d
		LayerGround = LayerMask.NameToLayer ("Ground");	//gets layer "Ground" from unity
		LayerPlayer = LayerMask.NameToLayer("Player");
		rb.freezeRotation = true;
		// Create AI pattern TESTING


	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Update for physics stuffs
	void FixedUpdate(){
		baddieKeepAway ();
	}

	//*******************Helper Functions******************//

	// Simple Baddie AI
	void baddieKeepAway(){
		// Set/update Raycast line
		keepAwayStart = transform.position;
		keepAwayStart.x -= xOffset;
		keepAwayEnd = transform.position;
		keepAwayEnd.x -= keepAwayEndDist;

		Debug.DrawLine(keepAwayStart, keepAwayEnd, Color.red);
		RaycastHit2D playerIsClose = Physics2D.Linecast(keepAwayStart, keepAwayEnd);

		if (playerIsClose) {
			if (playerIsClose.transform.gameObject.layer == LayerPlayer) {
				transform.position += Vector3.right * speed * Time.deltaTime; // Move baddie away!
				Debug.Log ("ON YOUR LEFT");
			}
		}

		keepAwayStart = transform.position;
		keepAwayStart.x += xOffset;
		keepAwayEnd = transform.position;
		keepAwayEnd.x += keepAwayEndDist;

		Debug.DrawLine(keepAwayStart, keepAwayEnd, Color.red);
		playerIsClose = Physics2D.Linecast(keepAwayStart, keepAwayEnd);

		if (playerIsClose) {
			if (playerIsClose.transform.gameObject.layer == LayerPlayer) {
				transform.position += Vector3.left * speed * Time.deltaTime; // Move baddie away!
				Debug.Log ("ON YOUR RIGHT");
			}
		}
	}


//	void baddieJump() {	//TODO: Some condition to make baddie jump
//		// Jump
//		bool placeholderCondition = false;
//		if (placeholderCondition)  {	// Some condition when baddie should jump
//
//			// Set/update Raycast line
//			groundStart = transform.position;
//			groundStart.y -= yOffset;
//			groundEnd = transform.position;
//			groundEnd.y -= groundEndDist;
//
//			//Debug.DrawLine(groundStart, groundEnd, Color.green);
//
//			RaycastHit2D groundHit = Physics2D.Linecast(groundStart, groundEnd);
//
//			//Debug.Log ("GROUNDHIT: " + groundHit.transform.gameObject.layer);
//			//Debug.Log ("LAYERGROUND: " + LayerGround);
//
//
//			if (Physics2D.Linecast (groundStart, groundEnd)) {
//				if (groundHit.transform.gameObject.layer == LayerGround) {
//					onGround = true;
//					rb.AddForce (Vector2.up * jump); // Add impulse
//					Debug.Log ("GROUNDED YO");
//				}
//			}
//
//			onGround = false;
//			Debug.Log ("GROUNDED NOOOOOOO");
//		}
//	}
}
