using UnityEngine;
using System.Collections;

public class PowaShootHarder : MonoBehaviour
{

    private int LayerPlayer;
    private Projectile projectileScript; // To change values on bullet
    public int dmg = 1;

    // Use this for initialization
    void Start()
    {
        LayerPlayer = LayerMask.NameToLayer("Player");
     
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("HARDER PEW");
            Destroy(gameObject);
            projectileScript.dmg += 1;
        }
    }
}