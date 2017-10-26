﻿using UnityEngine;
using System.Collections;

// Goomba style movement
public class BaddiePatrol: MonoBehaviour {	//TODO: Inherit Body class?? e.g code clean up

	public float speed = 1.5f;
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

	// For Cliff Raycast TODO: Possibly rally point system instead of dynamic?
	private int LayerPlayer;
	public float xOffset = 1.0f; // Offset for baddie sprite
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
		LayerGround = LayerMask.NameToLayer ("Ground");	//gets layer "Ground" from unity
		LayerPlayer = LayerMask.NameToLayer("Player");



	}

	// Update is called once per frame
	void Update () {

	}

	// Update for physics stuffs
	void FixedUpdate(){
		baddiePatrol ();
	}

	//*******************Helper Functions******************//

	// Simple Baddie AI
	void baddiePatrol(){
		// Set/update Raycast line
		cliffStart = transform.position;
		cliffStart.x -= xOffset;
		cliffEnd = transform.position;
		cliffEnd.x -= xOffset;
		cliffEnd.y -= cliffEndDist;

		Debug.DrawLine(cliffStart, cliffEnd, Color.blue);
		RaycastHit2D hit = Physics2D.Linecast(cliffStart, cliffEnd);

		if (Physics2D.Linecast (cliffStart, cliffEnd)) {
			if (hit.transform == null) {	// TODO: Not entering if
				toTheRight = true;
				Debug.Log ("To the windoooooOOOoOOoW");
			}
			if (hit.collider == true) {
				Debug.Log ("I'm hitting shit");
			}
			//Debug.Log (hit.collider.name);
		}

		cliffStart = transform.position;
		cliffStart.x += xOffset;
		cliffEnd = transform.position;
		cliffEnd.x += xOffset;
		cliffEnd.y -= cliffEndDist;

		Debug.DrawLine(cliffStart, cliffEnd, Color.blue);
		hit = Physics2D.Linecast(cliffStart, cliffEnd);

		if (Physics2D.Linecast (cliffStart, cliffEnd)) {
			if (!hit) {
				toTheRight = false;
				//Debug.Log ("ON YOUR LEFT");
			}
			//Debug.Log (hit.collider.name);
		}

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
}