using UnityEngine;
using System.Collections;

public class PowaJumpMore : MonoBehaviour {

	private int LayerPlayer;
    private GameObject thePlayer;
    private Controller playerScript; // To change any values on player
    public int jumpLimit = 2;

    // Use this for initialization
    void Start () {
		LayerPlayer = LayerMask.NameToLayer("Player");
        thePlayer = GameObject.Find("Player");
        playerScript = thePlayer.GetComponent<Controller>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			Debug.Log ("WE TRIPLE JUMP NOW");
            Destroy(gameObject);
            playerScript.jumpLimit += 1;
		}
	}
}
