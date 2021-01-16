using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour{
    private Transform player;
	private Animator anim;

	public bool isFlipped = false;

	private int health = 500;
	public bool isDead = false;

	void Start(){
		player = GameObject.FindGameObjectWithTag("Player").transform;
		anim = GetComponent<Animator>();
	}

	public void LookAtPlayer(){
		Vector3 flipped = transform.localScale;
		flipped.z *= -1f;

		if (transform.position.x > player.position.x && isFlipped){
			transform.localScale = flipped;
			transform.Rotate(0f, 180f, 0f);
			isFlipped = false;
		}else if (transform.position.x < player.position.x && !isFlipped){
			transform.localScale = flipped;
			transform.Rotate(0f, 180f, 0f);
			isFlipped = true;
		}
	}

    public void TakeDamage(int damage){
		health -= damage;
		if (health <= 0){
			Die();
		}
	}

	void Die(){
		if(!isDead){
			anim.SetTrigger("Dead");
			Destroy(GetComponent<BoxCollider2D>()); // Remove the hitbox, but leave the corpse
			StartCoroutine(RemoveEnemy());
		}
		isDead = true;
	}

	IEnumerator RemoveEnemy(){
		yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length + 1f); // Wait for the Dead animation to finish

		// Animate the dead enemy before removing him
		SpriteRenderer[] srs = GetComponentsInChildren<SpriteRenderer>();
		foreach (SpriteRenderer sr in srs){
			Color c = sr.color;
			c.a = 0;
			sr.color = c;
		}
		yield return new WaitForSeconds(.1f);
		foreach (SpriteRenderer sr in srs){
			Color c = sr.color;
			c.a = 1;
			sr.color = c;
		}
		yield return new WaitForSeconds(.1f);
		foreach (SpriteRenderer sr in srs){
			Color c = sr.color;
			c.a = 0;
			sr.color = c;
		}
		yield return new WaitForSeconds(.1f);

		Destroy(gameObject);
	}

}
