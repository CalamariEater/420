using UnityEngine;
using System.Collections;

// Goomba style movement
public class BaddiePatrol: MonoBehaviour {	//TODO: Inherit Body class?? e.g code clean up

	// Baddie stats
	public int hp = 10;
	public float speed = 1.5f;
	public float jump = 200.0f;
	public float exp = 1.0f;

	public Collider2D cdr;
	public Rigidbody2D rb;
	private SpriteRenderer spr; // To change spritecolor
	private GameObject thePlayer;
	private Controller playerScript; // To change any values on player
	private Color defaultColor;

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
		thePlayer = GameObject.Find ("Player");
		playerScript = thePlayer.GetComponent<Controller> ();
		LayerGround = LayerMask.NameToLayer ("Ground");	//gets layer "Ground" from unity
		LayerPlayer = LayerMask.NameToLayer("Player");
		rb.freezeRotation = true;

		defaultColor = spr.color; // Save default color
	}

	// Update is called once per frame
	void Update () {

	}

	// Update for physics stuffs
	void FixedUpdate(){
		baddiePatrol ();
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "projectile") { // Collision for player bullet
			//Debug.Log ("YO HIT ME DOOOD WOOOOwooOowoOWow");
			Destroy (coll.gameObject); // Destroy bullet
			StartCoroutine(flicker (2));

			hp--;
			if (hp <= 0) { // Death
				Destroy(gameObject);
				playerScript.exp += exp; 
			}
		}
	}
		

	//*******************Helper Functions******************//

	// Simple Baddie AI
	void baddiePatrol(){
		// Determine if left side is cliff
		cliffStart = transform.position;
		cliffStart.x -= xOffset;
		cliffStart.y -= yOffset;
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
		cliffStart.y -= yOffset;
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

	IEnumerator flicker(int blink){
		for (int i = 0; i < blink; i++) {
			spr.color = Color.red;
			yield return new WaitForSeconds(0.1f);
			spr.color = Color.white;
			yield return new WaitForSeconds(0.1f);
		}

		spr.color = defaultColor;
	}


}
