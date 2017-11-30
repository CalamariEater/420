using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour{


	public void NewGameBtn(string newGameLevel){
		SceneManager.LoadScene(newGameLevel);
	}

	public void OptionsBtn(string newGameLevel){
		PlayerPrefs.SetInt("previousLevel", Application.loadedLevel);
		SceneManager.LoadScene(newGameLevel);
	}

	public void ExitGameBtn(){
		Application.Quit ();
	}

}