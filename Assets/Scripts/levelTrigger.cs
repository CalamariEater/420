using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelTrigger : MonoBehaviour {

	// Time stuff
	public float currTime;

	// Use this for initialization
	void Start () {
		currTime = PlayerPrefs.GetFloat ("currTime"); // Get current time
	}
	
	// Update is called once per frame
	void Update () {
		currTime += Time.deltaTime; // Inc time
	}

	[SerializeField]
	public string level;

	// Use this for initialization
	void OnCollisionEnter2D (Collision2D coll)
	{
        if (coll.gameObject.tag == "Player")
        {
			PlayerPrefs.SetFloat ("currTime", currTime); // Sets time
            Application.LoadLevel(level);
        }

		if (coll.gameObject.tag == "projectile") { // Check if baddie made collision
			Destroy(coll.gameObject);
		}
	}



}
