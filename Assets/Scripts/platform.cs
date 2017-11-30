using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platform : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D (Collision2D coll) {
		if (coll.gameObject.tag == "projectile") { // Check if baddie made collision
			Destroy(coll.gameObject);
		}
	}
}
