using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {	//TODO: Inherit Body class?? e.g code clean up

	public float speed = 2.0f;
	public float jump = 200.0f;
	public int hp = 10;
	public int jumps = 0;
	public int jumpLimit = 2;

	public float knockBack = 150.0f;

	private Collider2D cdr;
	private Rigidbody2D rb;
	private SpriteRenderer spr;

	// For Jump Raycast
	public bool onGround = false;
	public float yOffset = 0.1f; // Offset for player sprite ~ not dynamic
	private int LayerGround;
	public Vector2 groundStart;	// For jump raycast
	public Vector2 groundEnd;	// For jump raycast
	public float groundEndDist = 0.1f;	// For jump raycast

	// Bullet Stuff
	public GameObject pewPrefab;
	public Transform pewSpawnLeft;
	public Transform pewSpawnRight;
	public float pewSpeed = 3.0f;
	public float pewDespawnRate = 2.0f;
	public float pewFireRate = 0.5f;
	public bool allowFire = true;


	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> (); //assign rigidbody to 2d
		cdr = GetComponent<Collider2D> (); //assign collider to 2d
		spr = GetComponent<SpriteRenderer>();
		LayerGround = LayerMask.NameToLayer ("Ground");	//gets layer "Ground" from unity
		rb.freezeRotation = true;
	}
	
	// Update is called once per frame
	void Update () {

	}

	// Use for physics stuff
	void FixedUpdate () {
		playerMovement ();
		playerFire ();
	}

	void OnCollisionEnter2D (Collision2D coll) {
		if (coll.gameObject.tag == "baddieBod") {
			Debug.Log ("ow");
			StartCoroutine(flicker (2));

			hp--;
			if (hp <= 0) { // Death
				Destroy(gameObject);
			}
				
			var force = transform.position - coll.transform.position;
			force.Normalize ();
			rb.AddForce (force * knockBack);
		}
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

	void playerFire() {
		if (Input.GetKey (KeyCode.LeftArrow) && allowFire) {
			//Debug.Log ("PEW");
			//StopCoroutine (shootLeft ());
			StartCoroutine (shootLeft ()); // Allows fire rate
		} else if (Input.GetKey (KeyCode.RightArrow) && allowFire) {
			StartCoroutine (shootRight ());
		}
	}

	IEnumerator shootLeft() {
		allowFire = false;

		// Create pew from prefab
		var pew = (GameObject)Instantiate (pewPrefab, pewSpawnLeft.position, pewSpawnLeft.rotation);

		// Velocity to pew
		pew.GetComponent<Rigidbody2D>().velocity = pew.transform.right * -pewSpeed;

		//Debug.Log ("Waiting for next shot...");

		yield return new WaitForSeconds(pewFireRate);

		// Destroy pew
		Destroy(pew, pewDespawnRate);

		allowFire = true;
	}

	IEnumerator shootRight() { // Possible enable disable? Seems pretty op
		allowFire = false;

		// Create pew from prefab
		var pew = (GameObject)Instantiate (pewPrefab, pewSpawnRight.position, pewSpawnLeft.rotation);

		// Velocity to pew
		pew.GetComponent<Rigidbody2D>().velocity = pew.transform.right * pewSpeed;

		//Debug.Log ("Waiting for next shot...");

		yield return new WaitForSeconds(pewFireRate);

		// Destroy pew
		Destroy(pew, pewDespawnRate);

		allowFire = true;
	}

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
