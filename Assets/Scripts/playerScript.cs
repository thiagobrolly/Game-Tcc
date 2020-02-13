using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour {

	private Animator playerAnimator;
	private Rigidbody2D playerRb;

	public Transform groundCheck;
	public LayerMask whatIsGround;

	public float speed;
	public float jumpForce;

	public bool Grounded;
	public bool attacking;
	public bool lookLeft;
	public int idAnimation;
	private float h, v;
	public Collider2D standing, crounching;

	public Transform hand;
	private Vector3 dir = Vector3.right;
	public LayerMask interacao;
	public GameObject objetoInteracao;

	//SISTEMA DE ARMAS
	public GameObject[] armas;



	// Use this for initialization
	void Start () {

		playerRb = GetComponent<Rigidbody2D>();
		playerAnimator = GetComponent<Animator>();

		foreach (GameObject o in armas)
		{
			o.SetActive(false);
		}
		
	}

	void FixedUpdate() {
		Grounded = Physics2D.OverlapCircle(groundCheck.position, 0.02f, whatIsGround);
		playerRb.velocity = new Vector2(h * speed, playerRb.velocity.y);

		interagir();
	}
	
	// Update is called once per frame
	void Update () {

		h = Input.GetAxisRaw("Horizontal");
		v = Input.GetAxisRaw("Vertical");

		if(h > 0 && lookLeft == true && attacking == false){
			flip();
		}
		else if(h < 0 && lookLeft == false && attacking == false){
			flip();
		}

		if(v < 0){
			idAnimation = 2;
			if(Grounded == true){
				h = 0;
			}
		}
		else if(h !=0){
			idAnimation = 1;
		} else{
			idAnimation = 0;
		}

		if(Input.GetButtonDown("Fire1") && v >= 0 && attacking == false && Grounded == true && objetoInteracao == null){
			playerAnimator.SetTrigger("atack");
		}

		if(Input.GetButtonDown("Fire1") && v >= 0 && attacking == false && Grounded == true && objetoInteracao != null){
			objetoInteracao.SendMessage("interacao", SendMessageOptions.DontRequireReceiver);
		}


		else if(Input.GetButtonDown("Fire1") && attacking == false && Grounded == false){ 
			playerAnimator.SetTrigger("atackJump");
		}
		else if(Input.GetButtonDown("Fire1") && v < 0 && attacking == false && Grounded == true){ 
			playerAnimator.SetTrigger("atackCrouch");
		}

		

		if(Input.GetButtonDown("Jump") && Grounded == true && attacking == false){
			playerRb.AddForce(new Vector2(0, jumpForce));
		}

		if(attacking == true && Grounded == true){
			h = 0;
		}

		if(v < 0 && Grounded == true){
			crounching.enabled = true;
			standing.enabled = false;
		} else if(v >= 0 && Grounded == true){
			crounching.enabled = false;
			standing.enabled = true;
		} else if(v != 0 && Grounded == false){
			crounching.enabled = false;
			standing.enabled = true;
		}

		playerAnimator.SetBool("grounded", Grounded);
		playerAnimator.SetInteger("idAnimation", idAnimation);
		playerAnimator.SetFloat("speedY", playerRb.velocity.y);

		
	}

	void flip(){
		lookLeft = !lookLeft;
		float x = transform.localScale.x;
		x *= -1;
		transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);

		dir.x = x;
		
	}

	void atack(int atk){
		switch(atk){
			case 0:
				attacking = false;
				armas[4].SetActive(false);
				break;
			case 1:
				attacking = true;
				break;
		}
	}

	void interagir(){

		Debug.DrawRay(hand.position, dir * 0.2f, Color.red);
		RaycastHit2D hit = Physics2D.Raycast(hand.position, dir, 0.2f, interacao);

		if(hit == true){
			objetoInteracao = hit.collider.gameObject;
		}
		else{
			objetoInteracao = null;
		}
		
		
	}

	void controleArma(int id){

		foreach (GameObject o in armas)
		{
			o.SetActive(false);
		}

		armas[id].SetActive(true);
	}



}
