using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public CharacterController2D controller;

	public float runSpeed = 40f;

	float horizontalMove = 0f;
	bool jump = false;
	bool crouch = false;

	PlayerHealth playerHealth;

	void Start(){
		playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
	}
	
	// Update is called once per frame
	void Update () {

		if(playerHealth.health > 0){
			horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
			GetComponent<Animator>().SetFloat("horizontal", Mathf.Abs(horizontalMove));

			/*if (Input.GetButtonDown("Jump"))
			{
				jump = true;
			}*/

			/*if (Input.GetButtonDown("Crouch"))
			{
				crouch = true;
			} else if (Input.GetButtonUp("Crouch"))
			{
				crouch = false;
			}*/
		}
	}

	void FixedUpdate ()
	{
		// Move our character
		controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
		jump = false;
	}
}