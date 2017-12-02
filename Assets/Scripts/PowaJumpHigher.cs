using UnityEngine;
using System.Collections;

public class PowaJumpHigher : MonoBehaviour
{

    private int LayerPlayer;
    private GameObject thePlayer;
    private Controller playerScript; // To change any values on player
    public float jump = 200.0f;  // jump force

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
            playerScript.jump += jump;
            Destroy(gameObject);
            Debug.Log(jump);
        }
    }
}