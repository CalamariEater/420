using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour{

	void Start () {
		Time.timeScale = 1; //starts time should start game.
	}

	public void NewGameBtn(string newGameLevel){
        PlayerPrefs.SetFloat("currTime", 0f); // reset running time
		SceneManager.LoadScene(newGameLevel);
	}

	public void TutorialBtn(string newGameLevel){
        PlayerPrefs.SetFloat("currTime", 0f); // reset running time
        SceneManager.LoadScene(newGameLevel);
	}

	public void OptionsBtn(string newGameLevel){
		//PlayerPrefs.SetInt("previousLevel", Application.loadedLevel);
		//SceneManager.LoadScene(newGameLevel);
		SceneManager.LoadScene ("Options", LoadSceneMode.Additive);
	}

	public void ExitGameBtn(){
		Application.Quit ();
	}

}