using UnityEngine;
using System.Collections;

public class PowaJumpMore : MonoBehaviour {

	private int LayerPlayer;


	// Use this for initialization
	void Start () {
		LayerPlayer = LayerMask.NameToLayer("Player");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			Debug.Log ("POWA UUUUUP");

		}
	}
}
