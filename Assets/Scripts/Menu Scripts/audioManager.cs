using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class audioManager : MonoBehaviour {

	public AudioSource audio;

	// Use this for initialization
	void Start () {
		audio.volume = PlayerPrefs.GetFloat ("MasterVol");
	}
	
	// Update is called once per frame
	void Update () {
		audio.volume = PlayerPrefs.GetFloat ("MasterVol");
	}
}
