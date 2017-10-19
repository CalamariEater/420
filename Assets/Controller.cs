using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

	public float speed = 2.0f;
	public float jump = 200.0f;

	public Collider2D cdr;
	public Rigidbody2D rb;

	public bool onGround = false;

	public float playerOffset = 0.1f;

	private int LayerGround;
	public Vector2 groundStart;
	public Vector2 groundEnd;
	public float groundEndDist = 0.1f;

	//public float groundEndRay = -1.0f;

	// Ground raycast
	//groundStartCast.

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		cdr = GetComponent<Collider2D> ();
		LayerGround = LayerMask.NameToLayer ("Ground");
		//playerOffset = GetComponent<SpriteRenderer> ().sprite.rect.size.x / 2;
	}
	
	// Update is called once per frame
	void Update () {



		// Debug lines
		groundStart = transform.position;
		groundStart.y -= playerOffset;


		//groundEnd.y -= playerOffset;
		groundEnd = transform.position;
		groundEnd.y -= groundEndDist;

		Debug.DrawLine(groundStart, groundEnd, Color.green);

	}

	// Use for physics stuff
	void FixedUpdate () {
		// Left
		if (Input.GetKey (KeyCode.A)) {
			transform.position += Vector3.left * speed * Time.deltaTime;
		}

		// Right
		if (Input.GetKey (KeyCode.D)) {
			transform.position += Vector3.right * speed * Time.deltaTime;
		}

		// Jump
		if (Input.GetKeyDown (KeyCode.W))  {

			// Check if we are still on the ground
			// Debug lines
			groundStart = transform.position;
			groundStart.y -= playerOffset;


			//groundEnd.y -= playerOffset;
			groundEnd = transform.position;
			groundEnd.y -= groundEndDist;

			//Debug.DrawLine(groundStart, groundEnd, Color.green);

			RaycastHit2D groundHit = Physics2D.Linecast(groundStart, groundEnd);

			//Debug.Log ("GROUNDHIT: " + groundHit.transform.gameObject.layer);
			//Debug.Log ("LAYERGROUND: " + LayerGround);


			if (Physics2D.Linecast (groundStart, groundEnd)) {
				if (groundHit.transform.gameObject.layer == LayerGround) {
					onGround = true;
					rb.AddForce (Vector2.up * jump); // Add impulse
					Debug.Log ("GROUNDED YO");
				}
			}

			onGround = false;
			Debug.Log ("GROUNDED NOOOOOOO");
		}
	}


}
