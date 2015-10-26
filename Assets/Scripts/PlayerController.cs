using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float moveSpeed;
	public float jumpForce;
	public bool grounded;
	public LayerMask WhatIsGround;

	private Rigidbody2D myRigidbody;
	private Collider2D myCollider;
	private Animator myAnimator;

	// Use this for initialization
	void Start () {
		myRigidbody = GetComponent<Rigidbody2D> ();
		myCollider = GetComponent<Collider2D> ();
		myAnimator = GetComponent<Animator> ();
		grounded = false;
	
	}
	
	// Update is called once per frame
	void Update () {
		myRigidbody.velocity = new Vector2(moveSpeed, myRigidbody.velocity.y);
		grounded = Physics2D.IsTouchingLayers (myCollider, WhatIsGround);
		
		if (grounded && Input.GetKeyDown (KeyCode.Space)) {
			myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
		}

		myAnimator.SetFloat("moveSpeed", moveSpeed);
		myAnimator.SetBool("grounded", grounded);
	
	}
}
