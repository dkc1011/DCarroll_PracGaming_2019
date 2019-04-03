using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    PlayerControl myPlayer;
    private int health;


	// Use this for initialization
	void Start () {
        health = 100;
        myPlayer  = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();
    }
	
	// Update is called once per frame
	void Update () {
        if (myPlayer.GetEnemyHit())
        {
            if(health > 0)
            {
                TakeDamage(myPlayer.GetWeaponDamage());
                print("Enemy was hit!");
            }
            else
            {
                Die();
            }

            myPlayer.SetEnemyHit(1);

        }
	}

    private void TakeDamage(int wepDamage)
    {
        health -= wepDamage;
    }

    private void Die()
    {
        this.gameObject.SetActive(false);
        print("Enemy is dead");
    }

}
