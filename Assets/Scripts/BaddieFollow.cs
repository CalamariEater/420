using UnityEngine;
using System.Collections;

public class BaddieFollow: MonoBehaviour {	//TODO: Inherit Body class?? e.g code clean up

	public float speed = 2.0f;
	public float jump = 200.0f;
	public float exp = 1.0f;
	public int hp = 10;

	public Collider2D cdr;
	public Rigidbody2D rb;
	private GameObject thePlayer;
	private Controller playerScript; // To change any values on player
	private SpriteRenderer spr; // To change spritecolor
    private Color defaultColor; // To reset color back for flicker

	// For Jump Raycast
	private int LayerGround;
	public float yOffset = 0.1f; // Offset for baddie sprite
	public bool onGround = false;
	public Vector2 groundStart;	// For jump raycast
	public Vector2 groundEnd;	// For jump raycast
	public float groundEndDist = 0.1f;	// For jump raycast

	// For Follow Raycast
	private int LayerPlayer;
	public float xOffset = 3.0f; // Offset for baddie sprite ~ Follow distance
	public bool tooClose = false;
	public Vector2 followStart;
	public Vector2 followEnd;
	public float followEndDist = 0.1f; // point closest to baddie

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> (); //assign rigidbody
		cdr = GetComponent<Collider2D> (); //assign collider
        spr = GetComponent<SpriteRenderer>(); // assign sprite renderer
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
		baddieFollow ();
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
	void baddieFollow(){
		// Set/update Raycast line
		followStart = transform.position;
		followStart.x -= xOffset;
		followEnd = transform.position;
		followEnd.x -= followEndDist;

		Debug.DrawLine(followStart, followEnd, Color.blue);
		RaycastHit2D playerIsClose = Physics2D.Linecast(followEnd, followStart);

		if (playerIsClose) {
			if (playerIsClose.transform.gameObject.layer == LayerPlayer) {
				transform.position += Vector3.left * speed * Time.deltaTime; // Move baddie
				//Debug.Log ("ON YOUR LEFT");
				//Debug.Log(playerIsClose.transform.parent.name);
			}
		}

		followStart = transform.position;
		followStart.x += xOffset;
		followEnd = transform.position;
		followEnd.x += followEndDist;

		Debug.DrawLine(followStart, followEnd, Color.blue);
		playerIsClose = Physics2D.Linecast(followEnd, followStart);

		if (playerIsClose) {
			if (playerIsClose.transform.gameObject.layer == LayerPlayer) {
				transform.position += Vector3.right * speed * Time.deltaTime; // Move baddie
				//Debug.Log ("ON YOUR RIGHT");
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
