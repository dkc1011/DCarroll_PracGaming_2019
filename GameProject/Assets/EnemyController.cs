using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    PlayerControl myPlayer;
    private int health;
    private char facing;
    float enemySpeed;
    private Vector3 startPosition;
    private Vector3 targetPosition;
    private enum status { Patrolling, Alerted, Engaging }
    private status enemyStatus;

	// Use this for initialization
	void Start () {
        facing = 'r';
        health = 100;
        myPlayer  = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();
        targetPosition = new Vector3(startPosition.x, startPosition.y, startPosition.z - 5);
        enemyStatus = status.Patrolling;
        startPosition = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        switch(enemyStatus)
        {
            case status.Patrolling:
                if(targetPosition.z <= transform.position.z)
                {
                    PatrolLeft();
                }
                else if(targetPosition.z >= transform.position.z)
                {
                    PatrolRight();
                }
                else if(targetPosition.z == transform.position.z && targetPosition.x <= transform.position.x)
                {
                    PatrolDown();
                }
                else if (targetPosition.z == transform.position.z && targetPosition.x >= transform.position.x)
                {
                    PatrolUp();
                }

                break;

            case status.Alerted:


            case status.Engaging:

            default:
                enemyStatus = status.Patrolling;
                break;
        }
	}
    private void Move(float enemySpeed)
    {
        transform.position += enemySpeed * transform.forward * Time.deltaTime;
    }//End Move()

    private void TakeDamage(int wepDamage)
    {

    }

    private void Die()
    {

    }

    private void PatrolLeft()
    {
        FaceLeft();

        Move(enemySpeed);
    }

    private void PatrolRight()
    {
        FaceRight();

        Move(enemySpeed);
    }

    private void PatrolUp()
    {
        FaceIn();

        Move(enemySpeed);
    }

    private void PatrolDown()
    {
        FaceOut();

        Move(enemySpeed);
    }

    private void FaceRight()
    {
        if (facing != 'r')
        {
            if (facing == 'u')
            {
                transform.Rotate(Vector3.up, 90);
                facing = 'r';
            }
            else if (facing == 'd')
            {
                transform.Rotate(Vector3.up, 270);
                facing = 'r';
            }
            else if (facing == 'l')
            {
                transform.Rotate(Vector3.up, 180);
                facing = 'r';
            }
        }
        else
        {
            facing = 'r';
        }//End If/Else
    }

    private void FaceLeft()
    {
        if (facing != 'l')
        {
            if (facing == 'u')
            {
                transform.Rotate(Vector3.up, 270);
                facing = 'l';
            }
            else if (facing == 'd')
            {
                transform.Rotate(Vector3.up, 90);
                facing = 'l';
            }
            else if (facing == 'r')
            {
                transform.Rotate(Vector3.up, 180);
                facing = 'l';
            }
        }
        else
        {
            facing = 'l';
        }//End If/Else
    }

    private void FaceIn()
    {
        if (facing != 'u')
        {
            if (facing == 'r')
            {
                transform.Rotate(Vector3.up, -90);
                facing = 'u';
            }
            else if (facing == 'd')
            {
                transform.Rotate(Vector3.up, 180);
                facing = 'u';
            }
            else if (facing == 'l')
            {
                transform.Rotate(Vector3.up, 90);
                facing = 'u';
            }
        }
        else
        {
            facing = 'u';
        }//End If/Else
    }

    private void FaceOut()
    {
        if (facing != 'd')
        {
            if (facing == 'r')
            {
                transform.Rotate(Vector3.up, 90);
                facing = 'd';
            }
            else if (facing == 'u')
            {
                transform.Rotate(Vector3.up, 180);
                facing = 'd';
            }
            else if (facing == 'l')
            {
                transform.Rotate(Vector3.up, -90);
                facing = 'd';
            }
        }
        else
        {
            facing = 'd';
        }
    }

}
