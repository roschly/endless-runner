using UnityEngine;
using System.Collections;

//On ground -3.235
//crouch -2.29

public class JumpableScript : MonoBehaviour {


	public GameObject platformDestructionPoint;
	

	void Start () {
		platformDestructionPoint = GameObject.Find ("PlatformDestructionPoint");
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		
		if (other.tag == "Player") {
			
			//Application.LoadLevel(1);
			//Debug.Log("av");
			Application.LoadLevel("GameOverScene");
			return;
			
		}	
		
	}

	void Update () {
		if (transform.position.x < platformDestructionPoint.transform.position.x) {
			Destroy(gameObject);	
			
		}
		
	}
	
}