using UnityEngine;
using System.Collections;

public class SpringScript : MonoBehaviour {
	public Animator anims;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnTriggerStay2D(Collider2D other){
		if (other.gameObject.tag == "Player") {

				anims.SetBool ("Bounce", true);

		
			}
		}


	void OnTriggerExit2D(Collider2D other){
		if (other.gameObject.tag == "Player") {

			anims.SetBool ("Bounce", false);
			anims.SetBool ("Ground", false);
			anims.SetBool ("doubleJump", false);
			anims.SetBool ("tripleJump", false);
			
			
		}
	}

}
