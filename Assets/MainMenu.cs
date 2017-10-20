using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour {

	public void NGButton(string newGameLevel){
		SceneManager.LoadScene(newGameLevel);
	}

	public void ExitButton(){
		Application.Quit();
	}

}
