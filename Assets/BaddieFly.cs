using UnityEngine;
using System.Collections;

// Goomba style movement
public class BaddieFly: MonoBehaviour {	//TODO: Inherit Body class?? e.g code clean up

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

	private int LayerPlayer;

	// for fly
	private Transform theTransform;
	public int rotationSpeed = 3; //speed of turning

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> (); //assign rigidbody to 2d
		cdr = GetComponent<Collider2D> (); //assign collider to 2d
		spr = GetComponent<SpriteRenderer>();
		thePlayer = GameObject.Find ("Player");
		playerScript = thePlayer.GetComponent<Controller> ();
		LayerPlayer = LayerMask.NameToLayer("Player");
		theTransform = gameObject.transform;
		//rb.freezeRotation = true;

	}

	// Update is called once per frame
	void Update () {

	}

	// Update for physics stuffs
	void FixedUpdate(){
		baddieFly ();
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
	void baddieFly(){/*
		theTransform.rotation = Quaternion.Slerp(theTransform.rotation,
			Quaternion.LookRotation(thePlayer.gameObject.transform - theTransform.position), rotationSpeed*Time.deltaTime);

		//move towards the player
		theTransform.position += theTransform.forward * speed * Time.deltaTime;*/
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
