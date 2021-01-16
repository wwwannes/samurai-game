using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int health;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;  

	void Start(){
		health = 3;

		for (int i = 0; i < hearts.Length; i++){
            hearts[i].sprite = fullHeart;
        }
	}

	public void TakeDamage(int damage){
		health -= damage;

		hearts[health].sprite = emptyHeart;
		hearts[health].enabled = true;

		StartCoroutine(DamageAnimation());

		if (health <= 0){
			Die();
		}
	}

	void Die(){
		GetComponent<Animator>().SetTrigger("Dead");
		//SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	IEnumerator DamageAnimation(){
		SpriteRenderer[] srs = GetComponentsInChildren<SpriteRenderer>();

		for (int i = 0; i < 3; i++)
		{
			foreach (SpriteRenderer sr in srs)
			{
				Color c = sr.color;
				c.a = 0;
				sr.color = c;
			}

			yield return new WaitForSeconds(.1f);

			foreach (SpriteRenderer sr in srs)
			{
				Color c = sr.color;
				c.a = 1;
				sr.color = c;
			}

			yield return new WaitForSeconds(.1f);
		}
	}
}
