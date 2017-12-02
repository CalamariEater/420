using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class playVid : MonoBehaviour {

	public MovieTexture movie;
    public string level;

	// Use this for initialization
	void Start () {
		GetComponent<RawImage>().texture = movie as MovieTexture;
		movie.Play ();
	}

	void Update () {
        if (!movie.isPlaying)
        {
            Application.LoadLevel(level);
        }

	}

}﻿
