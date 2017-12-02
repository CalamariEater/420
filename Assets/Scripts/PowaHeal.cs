using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowaHeal : MonoBehaviour {

    private int LayerPlayer;
    private GameObject thePlayer;
    private Controller playerScript; // To change any values on player
    public int hp = 10;

    // Use this for initialization
    void Start()
    {
        LayerPlayer = LayerMask.NameToLayer("Player");
        thePlayer = GameObject.Find("Player");
        playerScript = thePlayer.GetComponent<Controller>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("HEAL");
            Destroy(gameObject);
            playerScript.hp += 1;
        }
    }
}
