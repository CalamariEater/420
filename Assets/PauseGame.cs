using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour {
	public Transform canvas;
	public Transform Player;

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Pause ();
		}
	}

	public void Pause(){

		if (canvas.gameObject.activeInHierarchy == false) {
			canvas.gameObject.SetActive (true);
			Time.timeScale = 0; //stops time should stop game

		} else {
			canvas.gameObject.SetActive (false);
			Time.timeScale = 1; //starts time should start game.
		}

	}

	public void OptionsBtn(string newGameLevel){
		PlayerPrefs.SetInt("previousLevel", Application.loadedLevel);
		SceneManager.LoadScene(newGameLevel);
	}

	public void ReturnBtn(string newGameLevel){
		SceneManager.LoadScene(newGameLevel);
	}
}
