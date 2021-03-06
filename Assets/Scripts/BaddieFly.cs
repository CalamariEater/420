﻿using UnityEngine;
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
	private Color defaultColor;

	private int LayerPlayer;

	// for fly
	public float distance = 0.1f;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> (); //assign rigidbody to 2d
		cdr = GetComponent<Collider2D> (); //assign collider to 2d
		spr = GetComponent<SpriteRenderer>();
		thePlayer = GameObject.Find ("Player");
		playerScript = thePlayer.GetComponent<Controller> ();
		LayerPlayer = LayerMask.NameToLayer("Player");
		rb.freezeRotation = true;
		defaultColor = spr.color; // Save default color


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

			rb.velocity = Vector2.zero;

			Destroy (coll.gameObject); // Destroy bullet
			StartCoroutine(flicker (2));

			hp--;
			if (hp <= 0) { // Death
				Destroy(gameObject);
				playerScript.exp += exp; 
			}
		}


	}

	void OnCollisionStay2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "killBaddieSlow") {
			int num = Random.Range(1,250);
			Debug.Log ("fk me in the butt");
			Debug.Log (num);
			if (num == 1) {
				hp = hp - 1;
			}

			if (hp <= 0) { // Death
				Destroy(gameObject);
			}
		}
	}


	//*******************Helper Functions******************//

	// Simple Baddie AI
	void baddieFly(){
		transform.LookAt (thePlayer.transform.position);
		transform.Rotate(new Vector3(0,-90,0),Space.Self);//correcting the original rotation

		//move towards the player
		if (Vector3.Distance(transform.position,thePlayer.transform.position)>distance){//move if distance from target is greater then distance

			transform.Translate(new Vector3(speed* Time.deltaTime,0,0) );
			//transform.position += transform.forward * speed * Time.deltaTime;
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
