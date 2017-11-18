using System;
using UnityEngine;
using System.Collections;


public class Body : MonoBehaviour	// Should REALLY do inheritance shit
{
	public float speed = 2.0f;
	public float jump = 200.0f;
	public int hp = 10;

	public Collider2D cdr;
	public Rigidbody2D rb;

	// For Jump Raycast
	public bool onGround = false;
	public float yOffset = 0.1f; // Offset for body sprite ~ not dynamic
	public int LayerGround;
	public Vector2 groundStart;	// For jump raycast
	public Vector2 groundEnd;	// For jump raycast
	public float groundEndDist = 0.1f;	// For jump raycast

	public Body ()
	{
		rb = GetComponent<Rigidbody2D> (); //assign rigidbody to 2d
		cdr = GetComponent<Collider2D> (); //assign collider to 2d
		LayerGround = LayerMask.NameToLayer ("Ground");	//gets layer "Ground" from unity
		//rb.freezeRotation = true;
	}
}
	