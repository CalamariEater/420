using UnityEngine;
using System.Collections;

// Goomba style movement
public class BaddiePatrol: MonoBehaviour {	//TODO: Inherit Body class?? e.g code clean up

	public int hp = 10;
	public float speed = 1.5f;
	public float jump = 200.0f;

	public Collider2D cdr;
	public Rigidbody2D rb;
	private SpriteRenderer spr;

	// For Jump Raycast
	private int LayerGround;
	public float yOffset = 0.1f; // Offset for baddie sprite
	public bool onGround = false;
	public Vector2 groundStart;	// For jump raycast
	public Vector2 groundEnd;	// For jump raycast
	public float groundEndDist = 0.1f;	// For jump raycast

	// For Cliff Raycast
	private int LayerPlayer;
	public float xOffset = 0.5f; // Offset for baddie sprite ~ determines edge detection
	//public float yCliffOffset = 3.0f; 
	public bool tooClose = false;
	public Vector2 cliffStart;
	public Vector2 cliffEnd;
	public float cliffEndDist = 1.0f;
	bool toTheRight = false;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> (); //assign rigidbody to 2d
		cdr = GetComponent<Collider2D> (); //assign collider to 2d
		spr = GetComponent<SpriteRenderer>();
		LayerGround = LayerMask.NameToLayer ("Ground");	//gets layer "Ground" from unity
		LayerPlayer = LayerMask.NameToLayer("Player");
		rb.freezeRotation = true;

	}

	// Update is called once per frame
	void Update () {

	}

	// Update for physics stuffs
	void FixedUpdate(){
		baddiePatrol ();
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "projectile") {
			Debug.Log ("YO HIT ME DOOOD WOOOOwooOowoOWow");
			Destroy (coll.gameObject);
			StartCoroutine(flicker (2));

			hp--;
			if (hp <= 0) { // Death
				Destroy(gameObject);
			}
		}
	}
		

	//*******************Helper Functions******************//

	// Simple Baddie AI
	void baddiePatrol(){
		// Determine if left side is cliff
		cliffStart = transform.position;
		cliffStart.x -= xOffset;
		cliffEnd = transform.position;
		cliffEnd.x -= xOffset;
		cliffEnd.y -= cliffEndDist;

		Debug.DrawLine(cliffStart, cliffEnd, Color.blue);
		RaycastHit2D hit = Physics2D.Linecast(cliffStart, cliffEnd);

		if (hit) {
			// Do nothing
		} else {
			toTheRight = true;
			//Debug.Log ("To the windoooooOOOoOOoW");
		}

		// Determine if right side is cliff ~ could've used empty objects
		cliffStart = transform.position;
		cliffStart.x += xOffset;
		cliffEnd = transform.position;
		cliffEnd.x += xOffset;
		cliffEnd.y -= cliffEndDist;

		Debug.DrawLine(cliffStart, cliffEnd, Color.blue);
		hit = Physics2D.Linecast(cliffStart, cliffEnd);

		if (hit) {
		} else {
			toTheRight = false;
		}

		// Actual movement
		if (toTheRight) {
			transform.position += Vector3.right * speed * Time.deltaTime; // Move baddie
		} else {
			transform.position += Vector3.left * speed * Time.deltaTime; // Move baddie
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

	IEnumerator flicker(int blink){
		Color defaultColor = spr.color; // Save default color

		for (int i = 0; i < blink; i++) {
			spr.color = Color.red;
			yield return new WaitForSeconds(0.1f);
			spr.color = Color.white;
			yield return new WaitForSeconds(0.1f);
		}

		spr.color = defaultColor;
	}
}
