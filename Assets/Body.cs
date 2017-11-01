using System;
using UnityEngine;
using System.Collections;


public class Body : MonoBehaviour	// Might wanna do inheritance shit if im not too lazy
{
	public float speed = 2.0f;
	public float jump = 200.0f;
	public int hp = 10;

	public Collider2D cdr;
	public Rigidbody2D rb;

	// For Jump Raycast
	public bool onGround = false;
	public float yOffset = 0.1f; // Offset for body sprite ~ not dynamic
	private int LayerGround;
	public Vector2 groundStart;	// For jump raycast
	public Vector2 groundEnd;	// For jump raycast
	public float groundEndDist = 0.1f;	// For jump raycast

	public Body ()
	{
		
	}
}
	