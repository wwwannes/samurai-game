using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run : StateMachineBehaviour{
    public float speed = 2.5f;
	public float attackRange = 3f;

	private float totalTime = 0.2f;
	private float timeRemaining;

	Transform player;
	Rigidbody2D rb;
	Enemy enemy;
	PlayerHealth playerHealth;
	private LayerMask playerLayer;
	private float playerPos;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex){
		player = GameObject.FindGameObjectWithTag("Player").transform;
		rb = animator.GetComponent<Rigidbody2D>();
		enemy = animator.GetComponent<Enemy>();
		playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();

		speed = Random.Range(speed, speed + 1.5f);

		timeRemaining = totalTime;
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex){
		enemy.LookAtPlayer();

		if(enemy.isFlipped){
			playerPos = player.position.x - (attackRange - 0.5f);
		} else {
			playerPos = player.position.x + attackRange;
		}

		Vector2 target = new Vector2(playerPos, rb.position.y);
		Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
		rb.MovePosition(newPos);

		if (Vector2.Distance(player.position, rb.position) <= attackRange && playerHealth.health > 0){
			// Delay the attack for a small amount
			if(timeRemaining > 0){
				timeRemaining -= Time.deltaTime;
			} else {
				animator.SetTrigger("Attack");
				timeRemaining = totalTime;
			}
		} else if (playerHealth.health <= 0){
			animator.SetTrigger("Idle");
		}
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex){
		animator.ResetTrigger("Attack");
	}
}
