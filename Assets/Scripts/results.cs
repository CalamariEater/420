using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class results : MonoBehaviour {

    public Text timerText;
    public Text highscore;
    public Text newscore;
     

    // Use this for initialization
    void Start()
    {

        float current = PlayerPrefs.GetFloat("currTime");
        float fastest = PlayerPrefs.GetFloat("highscore");

        timerText.text = "Your Time: " + PlayerPrefs.GetFloat("currTime").ToString("F2");
        highscore.text = "Fastest Time: " + PlayerPrefs.GetFloat("highscore").ToString("F2");

        if (current >= fastest) {
            newscore.text = "New fastest time!!!!";
            fastest = current;
        }
        else {
            newscore.text = "";
        }

        PlayerPrefs.SetFloat("highscore", fastest);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
