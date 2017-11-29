using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {	//TODO: Inherit Body class?? e.g code clean up

	// Player stats
	public float speed = 2.0f; // move speed
	public float exp = 0.0f; // current exp
	public int lvl = 1; // current exp
	public int skillPoints = 0;
	public float jump = 200.0f; // jump force
	public int hp = 10;
	public int jumps = 0; // number of jumps currently taken
	public int jumpLimit = 2; // max number of jumps

	public float knockBack = 150.0f;
	private int lvlUp = 5;

	private Collider2D cdr;
	private Rigidbody2D rb;
	private SpriteRenderer spr;

	// For Jump Raycast

	[SerializeField]
	public Transform[] groundPoint;
	[SerializeField]
	public float groundRadius;
	[SerializeField]
	public LayerMask whatIsGround;
	private bool isgrund;



	// Bullet Stuff
	public GameObject pewPrefab; // What it shoots
	public Transform pewSpawnLeft; // Where bullet spawns
	public Transform pewSpawnRight; 
	public float pewSpeed = 3.0f;
	public float pewDespawnRate = 2.0f;
	public float pewFireRate = 0.5f;
	public bool allowFire = true;

	public float bulletSize = 0.2f;

	private Transform activePlatform;
	private Vector3 activeLocalPlatformPoint;
	private Vector3 activeGlobalPlatformPoint;
	private Vector3 lastPlatformVelocity;

	private bool IsGrounded(){
		if (rb.velocity.y <= 0) {
			foreach (Transform point in groundPoint) {
				Collider2D[] colliders = Physics2D.OverlapCircleAll (point.position, groundRadius, whatIsGround);
				for (int i = 0; i < colliders.Length; i++) {
					if (colliders[i].gameObject != gameObject) {
						return true;
					}
				}
			}
		}
		return false;
	}

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> (); //assign rigidbody to 2d
		cdr = GetComponent<Collider2D> (); //assign collider to 2d
		spr = GetComponent<SpriteRenderer>();

		rb.freezeRotation = true;
	}
	
	// Update is called once per frame
	void Update () {
		
		playerJump ();
		playerMovement ();
		playerFire ();
		// Level up check
		if (lvlUp < exp) {
			exp = 0; //reset current exp
			skillPoints++;
			lvlUp++; //scale exp
		}


	}

	// Use for physics stuff
	void FixedUpdate () {
		isgrund = IsGrounded ();
	}

	void OnCollisionEnter2D (Collision2D coll) {
		if (coll.gameObject.tag == "baddieBod") { // Check if baddie made collision
			Debug.Log ("ow");
			StartCoroutine(flicker (2));

			// HP stuff
			hp--; // depending on damage of attack change this
			if (hp <= 0) { // Death
				Destroy(gameObject);
			}

			// Knockback
			var force = transform.position - coll.transform.position;
			force.Normalize ();
			rb.AddForce (force * knockBack);
		}

		if (coll.gameObject.tag == "trampoline") { // Check if baddie made collision
			jumps = jumpLimit;
			Vector2 v = rb.velocity;
			v.y = 0.0f;	
			rb.velocity = v; // Set Y velocity to 0 ~ avoids spam jump high af bug
			rb.AddForce (new Vector2(0,400.0f));

		}

		if (coll.gameObject.tag == "stickywall") { // Check if baddie made collision

			jumps = 0;

		}
			
		if (coll.gameObject.tag == "movingPlatform") {
			transform.SetParent (coll.transform);
		}

		if (coll.gameObject.tag == "cup") {
			
		}
		// Add more cases based on tag (damage taken)
	}

	void OnCollisionExit2D (Collision2D coll) {
		if (coll.gameObject.tag == "movingPlatform") {
			transform.parent = null;
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


	}

	void playerJump() {
		// Jump
		if (Input.GetKeyDown (KeyCode.W) || Input.GetKeyDown(KeyCode.Space))  {
			Debug.Log ("JUMP pressed");
			if (isgrund) {
				Debug.Log ("isground");
				isgrund = false;
				jumps = 0;
			} else {
				Debug.Log ("not ground");
			}

			if (jumps < jumpLimit) {
				Vector2 v = rb.velocity;
				v.y = 0.0f;	
				rb.velocity = v; // Set Y velocity to 0 ~ avoids spam jump high af bug
				rb.AddForce (new Vector2(0,jump));
				jumps++;
				Debug.Log ("DOOOUBLE JUMP");
			} 
		}
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
		pew.GetComponent<Rigidbody2D>().velocity = pew.transform.right * -pewSpeed; // Speed
		pew.gameObject.transform.localScale = new Vector3 (bulletSize, bulletSize, transform.localScale.y); // Bullet size

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
		pew.GetComponent<Rigidbody2D>().velocity = pew.transform.right * pewSpeed; // Speed
		pew.gameObject.transform.localScale = new Vector3 (bulletSize, bulletSize, transform.localScale.y); // Change bullet size
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
