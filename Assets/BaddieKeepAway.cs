using UnityEngine;
using System.Collections;

public class BaddieKeepAway: MonoBehaviour {	//TODO: Inherit Body class?? e.g code clean up

	public float speed = 2.0f;
	public float jump = 200.0f;
	public float exp = 1.0f;
	public int hp = 10;

	public Collider2D cdr;
	public Rigidbody2D rb;
	private GameObject thePlayer;
	private Controller playerScript; // To change any values on player
	private SpriteRenderer spr; // To change spritecolor
	private Color defaultColor;


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
		spr = GetComponent<SpriteRenderer>(); //assign spriteRenderer
		thePlayer = GameObject.Find ("Player");
		playerScript = thePlayer.GetComponent<Controller> ();
		LayerGround = LayerMask.NameToLayer ("Ground");	//gets layer "Ground" from unity
		LayerPlayer = LayerMask.NameToLayer("Player");
		rb.freezeRotation = true;
		Color defaultColor = spr.color; // Save default color
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Update for physics stuffs
	void FixedUpdate(){
		baddieKeepAway ();
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
