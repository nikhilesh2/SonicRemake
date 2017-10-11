using UnityEngine;
using System.Collections;

public class guardLogicFlyGuard : MonoBehaviour {

	public Transform sightStart, sightEnd;
	
	
	public GameObject redFlag;
	public GameObject greenFlag;
	public GameObject poof;

	public Rigidbody2D projectile;

	public Rigidbody2D Bullet;
	public Transform Muzzle;
	
	public AudioClip poofSound;
	
	public bool spotted = false;
	public bool facingLeft = true;
	
	// Use this for initialization
	void Start () {
		
		InvokeRepeating ("Patrol", 0f, Random.Range (2f, 6f));
		
	}
	
	// Update is called once per frame
	void Update () {
		
		Raycasting ();
		Behaviours ();
		
	}
	
	void Raycasting(){
		Debug.DrawLine (sightStart.position, sightEnd.position, Color.green);
		spotted = Physics2D.Linecast (sightStart.position, sightEnd.position, 1<< LayerMask.NameToLayer("Player"));
		
	}
	
	void Behaviours(){
		if (spotted == true) {
			CancelInvoke();
			rigidbody2D.velocity = new Vector2(0,0);
			guardAttack();
			
		} else if (spotted == false) {
			//Patrol ();
			//Start ();
			//redFlag.SetActive (false);
			//greenFlag.SetActive (true);
			
			
		}
		
	}
	
	void Patrol(){
		facingLeft = !facingLeft;
		
		
		if (facingLeft == true) {
			
			rigidbody2D.velocity = new Vector2(-1,0);
			transform.eulerAngles = new Vector2(0,0);
		}
		else if(facingLeft == false) {
			
			rigidbody2D.velocity = new Vector2(1,0);
			transform.eulerAngles = new Vector2(0,180);
		}
		
		
	}
	
	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag == "Player"){
			
			
			poof.SetActive(true);
			audio.clip = poofSound;
			audio.PlayOneShot(poofSound,0.7f);
			Destroy(gameObject,0.15f);
			
		}
		
		
	}


	void guardAttack(){ 
		Rigidbody2D b = GameObject.Instantiate(Bullet, Muzzle.position, Muzzle.rotation) as Rigidbody2D;
		b.AddForce(2000 * b.transform.forward);
		}
	}
