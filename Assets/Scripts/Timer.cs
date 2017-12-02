using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public Text timerText;

	// Use this for initialization
	void Start () {
        timerText.text = "Seconds: " + PlayerPrefs.GetFloat("currTime").ToString("F2");
    }
	
	// Update is called once per frame
	void Update () {
        timerText.text = "Seconds: " + PlayerPrefs.GetFloat("currTime").ToString("F2");
    }

 
}
