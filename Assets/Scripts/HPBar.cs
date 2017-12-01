using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour {

	private GameObject thePlayer;
	private Controller playerScript; // To change any values on player
	public Slider theSlider; // access to the slider
	private int defaultHP = 10;

	private float previousHP;

	// Use this for initialization
	void Start () {
		thePlayer = GameObject.Find ("Player");
		playerScript = thePlayer.GetComponent<Controller> ();
		previousHP = 10; // get initial hp val
	}
	
	// Update is called once per frame
	void Update () {
		if (previousHP != playerScript.hp) {
			theSlider.value = playerScript.hp;
			previousHP = theSlider.value;
		}
	}
}
