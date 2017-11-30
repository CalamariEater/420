using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

	[SerializeField]
	public string level;

	// Use this for initialization
	void OnCollisionEnter2D (Collision2D coll)
	{
        if (coll.gameObject.tag == "Player")
        {
            Application.LoadLevel(level);
        }

		if (coll.gameObject.tag == "projectile") { // Check if baddie made collision
			Destroy(coll.gameObject);
		}
	}


}
