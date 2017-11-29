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
	void OnCollisionEnter2D (Collision2D Colider)
	{
		if(Colider.gameObject.tag == "Player");
		Application.LoadLevel(level);
	}


}
