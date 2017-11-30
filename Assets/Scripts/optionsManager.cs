using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class optionsManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Time.timeScale = 1; //starts time should start game.

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void DoneBtn(){
		int previousLevel = PlayerPrefs.GetInt ("previousLevel");
		Application.LoadLevel (previousLevel);
	}
}
