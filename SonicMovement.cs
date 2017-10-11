using UnityEngine;
using System.Collections;

public class Sonicmovement : MonoBehaviour {

	public float maxSpeed = 10f;
	bool facingRight = true;
	
	public Animator anim;
	
	public AudioClip collectSound;
	public AudioClip cubeSound;
	public AudioClip jumpSound; 
	public AudioClip springSound;
	
	bool grounded = false;
	public Transform groundCheck;
	float groundRadius = 0.2f;
	public LayerMask whatIsGround;
	public float  jumpForce = 700f;
	public float downForce = 0f;
	float moves = 1f;
	int counter = 0;
	
	
	public GameObject player;
	//for our GUIText object and our score
	public GUIElement gui;
	
	
	public int amountofStars = 0;
	
	bool doubleJump = false;
	bool tripleJump = false;
	
	void start(){
		
		anim = GetComponent<Animator>();

	}
	
	public void FixedUpdate(){
		
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
		anim.SetBool("Ground", grounded);
		
		if (grounded){ 
			anim.SetBool("doubleJump", false);
			anim.SetBool("tripleJump", false);
			doubleJump = false;
			tripleJump = false;
			
		}
		anim.SetFloat("vSpeed", rigidbody2D.velocity.y);
		
		
		
		float move = Input.GetAxis("Horizontal");
		
		
		anim.SetFloat("Speed", Mathf.Abs(move));
		
		rigidbody2D.velocity = new Vector2(move * maxSpeed, rigidbody2D.velocity.y);
		
		if(move > 0 && !facingRight){
			
			Flip();
			//moves*=-1;
		}
		else if(move < 0 && facingRight){
			//moves*=-1;
			Flip ();
		}
	}
	
	void Update(){
		if((grounded )&& Input.GetKeyDown(KeyCode.Space)){
			audio.clip = jumpSound;
			audio.Play ();
			rigidbody2D.AddForce(new Vector2(0,jumpForce));
			anim.SetBool("Ground", false);

			
		} 
		
		else if((grounded || !doubleJump)&& Input.GetKeyDown(KeyCode.Space)){
			anim.SetBool("Ground", false);
			audio.clip = jumpSound;
			audio.Play ();
			rigidbody2D.AddForce(new Vector2(0,jumpForce*moves));
			counter = 1;
			
			
			if(!doubleJump && !grounded){
				if(counter != 2){
				anim.SetBool("doubleJump", true);
				doubleJump = true;  
					counter = 2;
				}else{
					anim.SetBool("doubleJump", true);
					doubleJump = true;  
					counter = 0;
				}
				
			}
			
			
		}
		

		if(Input.GetKey(KeyCode.S) && grounded){
			
			anim.SetBool("SpeedUp",true);
			rigidbody2D.velocity = new Vector2(moves * maxSpeed/8,-downForce);
			
		}
		
		else{anim.SetBool("SpeedUp",false);}
		
		if(Input.GetKeyUp(KeyCode.S)){
			
			
		}
		
		Camera.main.transform.position = 
			new Vector3 (transform.position.x, transform.position.y+1, shadowController.me.playerZdepth - shadowController.me.cameraBuffer);
		
	}
	
	
	void Flip(){
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
		
		
	}
	
	void OnTriggerStay2D(Collider2D other){
		if (other.gameObject.tag =="platform"){
			
			transform.parent =  other.transform;
			
		}

			if(other.gameObject.tag =="redButton"){
				Application.LoadLevel("shadowMainScene");
			}
		
		
		if(other.gameObject.tag =="Spikes"){
			rigidbody2D.velocity = new Vector2(moves * maxSpeed, downForce/10);
			//Application.LoadLevel("GameOver");
			anim.SetBool("Death",true);
			anim.SetBool("Ground",true);
			anim.SetBool("doubleJump", true);
			anim.SetBool("tripleJump", false);
			
			
		}
		
		
		if(other.gameObject.tag =="springBoard"){
			rigidbody2D.velocity = new Vector2(moves * maxSpeed, downForce/15);
			
			anim.SetBool("Bounce", true);
			anim.SetBool("Ground",true);
			anim.SetBool("doubleJump", false);
			anim.SetBool("tripleJump", false);
			
			
		}
		
		
		if(other.gameObject.tag =="star"){
			audio.clip = collectSound;
			audio.Play ();
			print("star collected");
			
		}
		if (other.gameObject.tag =="powerCube"){
			
			audio.clip = cubeSound;
			audio.Play ();
			
		}
		
		
		
	}
	
	void OnTriggerExit2D(Collider2D other){
		if(other.gameObject.tag =="platform"){
			transform.parent = null;
			anim.SetBool("Death",false);
		}
		
		
		if(other.gameObject.tag =="springBoard"){

			
			audio.clip = springSound;
			audio.Play ();
			anim.SetBool("Bounce", false);
			anim.SetBool("Ground",false);
			anim.SetBool("doubleJump", true);
			anim.SetBool("tripleJump", true);
			
			
			
		}
		if(other.gameObject.tag == "Spikes"){
			rigidbody2D.velocity = new Vector2(moves * maxSpeed, downForce/10);
			//Application.LoadLevel("GameOver");
			anim.SetBool("Death",false);
			anim.SetBool("Ground",true);
			anim.SetBool("doubleJump", true);
			anim.SetBool("tripleJump", false);
			
			
		}
		
		
		if(other.gameObject.tag =="star"){
			Destroy(GameObject.FindWithTag("star"));
			
		}
		
	}
	void OnGUI(){
		gui.guiText.text = "Score:" + ((int)(amountofStars )).ToString ();
	}
	//this is generic function we can call to increase the score by an amount
	public void increaseScore(int amount){
		amountofStars += amount;
	}
	
	
	
	
}