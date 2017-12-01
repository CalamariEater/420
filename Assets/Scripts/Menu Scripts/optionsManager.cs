using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class optionsManager : MonoBehaviour {


	public Slider volumeSlider;
	public AudioSource myMusic;
	public Text volumeText;

	// Use this for initialization
	void Start () {
		//Time.timeScale = 1; //starts time should start game.
		volumeSlider.value = PlayerPrefs.GetFloat ("MasterVol");
		volumeText.text = "" + Mathf.Round (100 * volumeSlider.value);
	}
	
	// Update is called once per frame
	void Update () {
		PlayerPrefs.SetFloat("MasterVol", myMusic.volume);
		volumeText.text = "" + Mathf.Round (100 * volumeSlider.value);
		myMusic.volume = volumeSlider.value;
	}

	public void DoneBtn(){
		//int previousLevel = PlayerPrefs.GetInt ("previousLevel");
		//Application.LoadLevel (previousLevel);
		SceneManager.UnloadScene ("Options");

	}


}
