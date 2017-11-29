using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadingPlatform : MonoBehaviour {

    public float wait = 2.0f; // time to wait till respawn
    public float fadeOut = 2.0f; // In seconds
    public float fadeIn = 2f; // By multitudes of 0.1f (like plain flicker)
    public bool isSteppedOn = false;
    public bool killMe = false;

    private SpriteRenderer spr; // To change spritecolor
    private Color defaultColor;
    private float width;

    // Use this for initialization
    void Start () {
        spr = GetComponent<SpriteRenderer>();
        defaultColor = spr.color;
        width = GetComponent<SpriteRenderer>().bounds.size.x;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "projectile") 
        { // Check if pew made collision
            Destroy(coll.gameObject);
        }
        if (coll.gameObject.tag == "Player") // Player
        {
            if (!isSteppedOn)
            {
                StartCoroutine(fade());
            }
            else
            {
                // do nothing
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

    IEnumerator fade()
    {
        isSteppedOn = true;
        for (int i = 0; i < fadeOut; i++) // Fade out
        {
            spr.color = Color.yellow;
            yield return new WaitForSeconds(0.5f);
            spr.color = Color.white;
            yield return new WaitForSeconds(0.5f);
        }

        spr.color = Color.clear; // Make clear
        gameObject.GetComponent<BoxCollider2D>().isTrigger = true; // Make able to go through

        if(killMe) // check if we don't need to respawn
        {
            DestroyObject(gameObject);
        }

        yield return new WaitForSeconds(wait); // delay

        for (int i = 0; i < fadeIn; i++) // Fade in time
        {
            spr.color = Color.yellow;
            yield return new WaitForSeconds(0.1f);
            spr.color = Color.white;
            yield return new WaitForSeconds(0.1f);
        }

        // Reset stuff
        spr.color = defaultColor;
        gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        isSteppedOn = false;
    }
}
