using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPlatform : MonoBehaviour {

	public float speed = 0.01f;

	public bool destReached = false;

	public Vector3 pos1;
	public Vector3 pos2;

	public Vector3 position;

	// Use this for initialization
	void Start () {

		pos1 = gameObject.transform.GetChild (0).position;
		pos2 = gameObject.transform.GetChild (1).position;
	}
	
	// Update is called once per frame
	void Update () {
		if (destReached == false) {
			transform.position = Vector2.MoveTowards (transform.position, pos2, speed);

			if (gameObject.transform.position == pos2) {
				Debug.Log ("pos1 reached!");
				destReached = true;
			}
		} else {
			transform.position = Vector2.MoveTowards (transform.position, pos1, speed);

			if (gameObject.transform.position == pos1) {
				destReached = false;
			}
		}

		position = gameObject.transform.position;
	}

	void OnCollisionEnter2D (Collision2D coll) {
		if (coll.gameObject.tag == "projectile") { // Check if baddie made collision
			Destroy(coll.gameObject);
		}
	}


}
