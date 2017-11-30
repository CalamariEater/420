using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class optionsManager : MonoBehaviour {


	public Slider volumeSlider;
	public AudioSource myMusic;


	// Use this for initialization
	void Start () {
		//Time.timeScale = 1; //starts time should start game.
		volumeSlider.value = PlayerPrefs.GetFloat ("MasterVol");

	}
	
	// Update is called once per frame
	void Update () {
		myMusic.volume = volumeSlider.value;
	}

	public void DoneBtn(){
		//int previousLevel = PlayerPrefs.GetInt ("previousLevel");
		//Application.LoadLevel (previousLevel);
		PlayerPrefs.SetFloat("MasterVol", myMusic.volume);
		SceneManager.UnloadScene ("Options");

	}


}
