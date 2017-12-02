using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public Text timerText;

	// Use this for initialization
	void Start () {
        timerText.text = "Text: " + PlayerPrefs.GetFloat("currTime");
    }
	
	// Update is called once per frame
	void Update () {
        timerText.text = "Text: " + PlayerPrefs.GetFloat("currTime");
    }
}
