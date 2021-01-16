using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour{

    public Animator animator;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int attackDamage = 33;

    PlayerHealth playerHealth;
    public Text highscore;
    private int totalDeadEnemies;

    private float totalTime = 0.5f;
	private float timeRemaining;

    void Start(){
		playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        totalDeadEnemies = 0;
        highscore.text = totalDeadEnemies+" enemies defeated";
        timeRemaining = totalTime;
	}

    // Update is called once per frame
    void Update(){
        if(timeRemaining > 0){
			timeRemaining -= Time.deltaTime;
		} else {
            if(Input.GetKeyDown(KeyCode.Space) && playerHealth.health > 0){
                Attack();
                timeRemaining = totalTime;
            }
        }
    }

    void Attack(){
        // play animation
        animator.SetTrigger("Attack");

        // detect enemies
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach(Collider2D enemy in hitEnemies){
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
            if(enemy.GetComponent<Enemy>().isDead){
                totalDeadEnemies += 1;
                highscore.text = totalDeadEnemies+" enemies defeated";
            }
        }
    }

    // Vizualises the range of the attack
    private void OnDrawGizmosSelected(){
        if(attackPoint == null){
            return;
        }

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
