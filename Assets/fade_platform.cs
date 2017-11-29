using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fade_platform : MonoBehaviour {

	public int secsOut = 2;
	public int secsIn = 2;

	private SpriteRenderer spr; // To change spritecolor
	private Color defaultColor;

	// Use this for initialization
	void Start () {
		spr = GetComponent<SpriteRenderer>();
		defaultColor = spr.color; // Save default color
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D (Collision2D coll) {
		if (coll.gameObject.tag == "projectile") { // Check if baddie made collision
			Destroy(coll.gameObject);
		}
		if (coll.gameObject.tag == "Player") {
			StartCoroutine(disappear() );
		}
	}

	IEnumerator disappear(){

		for (int i = 0; i < secsOut; i++) {
			spr.color = Color.yellow;
			yield return new WaitForSeconds(0.5f);
			spr.color = Color.white;
			yield return new WaitForSeconds(0.5f);
		}
		gameObject.GetComponent<SpriteRenderer> ().color = Color.clear;
		gameObject.GetComponent<BoxCollider2D> ().isTrigger = true;
		yield return new WaitForSeconds (secsIn);

		for (int i = 0; i < secsIn; i++) {
			spr.color = Color.yellow;
			yield return new WaitForSeconds(0.1f);
			spr.color = Color.white;
			yield return new WaitForSeconds(0.1f);
		}

		gameObject.GetComponent<BoxCollider2D> ().isTrigger = false;
		spr.color = defaultColor;
	}


}
