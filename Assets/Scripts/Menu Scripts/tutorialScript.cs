using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tutorialScript : MonoBehaviour {
	public Text changeText;
	public Image background;
	// Use this for initialization
	void Start () {
		changeText.text = "To move use the 'A' 'S' 'D' keys\n" +
			"To jump use the 'spacebar' or 'W'";
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.A) ||Input.GetKeyDown (KeyCode.D)) {
			changeText.text = "To shoot use the arrow keys.";
		}
		if (Input.GetKeyDown (KeyCode.LeftArrow) ||Input.GetKeyDown (KeyCode.RightArrow)) {
			changeText.text = "SHO";
		}

	}
}