using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PlayerController : MonoBehaviour {

	public float baseMoveSpeed;
	private float moveSpeed;
	public float jumpForce;
	public bool grounded;
	public bool crouch;
	public LayerMask WhatIsGround;

	private Rigidbody2D myRigidbody;
	private CircleCollider2D myCircleCollider;
	private BoxCollider2D myBoxCollider;
	private Animator myAnimator;

	private Texture2D tex;
	private int picture_counter = 0;

	// Use this for initialization
	void Start () {
		tex = new Texture2D(500, 200, TextureFormat.RGB24, false);
		myRigidbody = GetComponent<Rigidbody2D> ();
		myCircleCollider = GetComponent<CircleCollider2D> ();
		myBoxCollider = GetComponent<BoxCollider2D> ();
		myAnimator = GetComponent<Animator> ();
		grounded = false;
		
		moveSpeed = baseMoveSpeed;
	
	}

	private IEnumerator captureScreen(){
		yield return new WaitForEndOfFrame();
		
		// Read screen contents into the texture
		tex.ReadPixels(new Rect(0, 70, 500, 270), 0, 0);
		
		tex.Apply ();

		// save images
		byte[] bytes = tex.EncodeToPNG();
		System.IO.File.WriteAllBytes(Application.dataPath + "/../Images/SavedScreen" + this.picture_counter + ".png", bytes);
		this.picture_counter++;

		
		/*
			Color test = new Color (0.247F, 0.710F, 0.851F, 1.000F);
			if (test.ToString () == bv.GetValue (86600).ToString ()) {
				m_Character.Move(1,false,true);
				//Debug.Log ("HEY HEY HEY HEY HEY HEY HEY HEY!!!!!");
			}
			*/

		//Debug.Log (test.ToString() + "  Bnana");
		//Debug.Log (bv.GetValue (86500).ToString());
	}


	private void decide(){
		Color[] bv = tex.GetPixels();
		
		List<double> output = BrainControlScript.brainControl.genome.sendThroughNetwork (bv);
		

		move (output[0] > 0.5, output[1] > 0.5);


		Debug.Log (output [0] + "     " + output [1]);


		if (output [0] > 0.5) {
			//Debug.Log("JUMP");
		}
		if (output [1] > 0.5) {
			//Debug.Log("crouch");
		}
		if (output [0] <= 0.5 && output [1] <= 0.5){
			//Debug.Log("run");
		}


		//Debug.Log (BrainControlScript.brainControl.genome.toString ());
		
	}


	private void move(bool jump, bool crouch){
		grounded = Physics2D.IsTouchingLayers (myBoxCollider, WhatIsGround);
	
		if (grounded && (Input.GetKey (KeyCode.LeftControl) || crouch )) {
			myBoxCollider.size = new Vector2((float)0.875, (float)0.9);
			myBoxCollider.offset = new Vector2((float)0, (float)0.459);
			moveSpeed = (float)0.5 * baseMoveSpeed;
		} else {
			myBoxCollider.size = new Vector2((float)0.828125, (float)1.148438);
			myBoxCollider.offset = new Vector2((float)0, (float)0.5742188);
			moveSpeed = baseMoveSpeed;
		}


		myRigidbody.velocity = new Vector2(moveSpeed, myRigidbody.velocity.y);
		
		if (grounded && (Input.GetKeyDown (KeyCode.Space) || jump)) {
			myRigidbody.velocity = new Vector2 (myRigidbody.velocity.x, jumpForce);
		}
		
		myAnimator.SetFloat("moveSpeed", moveSpeed);
		myAnimator.SetBool("grounded", grounded);
		myAnimator.SetBool("crouch", crouch);
	}

	private void updateFitness() {
		BrainControlScript.brainControl.fitness = (int)transform.position.x;
	}

	// Update is called once per frame
	void Update () {
		StartCoroutine (captureScreen());
		updateFitness ();

		//move (false);
		decide();
	}

}
