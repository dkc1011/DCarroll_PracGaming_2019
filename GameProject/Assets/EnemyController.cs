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
    private enum Status { Idle, Patrolling, Alerted, Engaging }
    private Status enemyStatus;
    private float waitTimer;
    private RaycastHit playerCheck;

    // Use this for initialization
    void Start () {
        facing = 'l';
        health = 100;
        myPlayer  = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();
        targetPosition = new Vector3(startPosition.x+3, startPosition.y, startPosition.z - 5);
        enemyStatus = Status.Idle;
        startPosition = transform.position;
        waitTimer = 0;

        
    }
	
	// Update is called once per frame
	void Update () {
        switch(enemyStatus)
        {
            case Status.Idle:
                Vector3 fwd = transform.forward;

                if (transform.position != startPosition)
                {
                    targetPosition = startPosition;
                    enemyStatus = Status.Patrolling;
                }
                else
                {
                    waitTimer += Time.deltaTime;

                    if (waitTimer >= 3 && waitTimer <= 6)
                    {
                        FaceOut();
                    }
                    else if (waitTimer > 6 && waitTimer <= 9)
                    {
                        FaceRight();
                    }
                    else if (waitTimer > 9 && waitTimer <= 12)
                    {
                        FaceIn();
                    }
                    else if (waitTimer > 12)
                    {
                        waitTimer = 0;
                        FaceLeft();
                    }                    


                    if (Physics.Raycast(transform.position, fwd, out playerCheck, 15))
                    {
                        if (playerCheck.collider.tag == "Player")
                        {
                            Debug.Log("Player spotted");

                            if (playerCheck.point.z < transform.position.z)
                            {
                                targetPosition = new Vector3(playerCheck.point.x, playerCheck.point.y, playerCheck.point.z + 2);
                            }
                            else if(playerCheck.point.z > transform.position.z)
                            {
                                targetPosition = new Vector3(playerCheck.point.x, playerCheck.point.y, playerCheck.point.z - 2);
                            }
                               

                            enemyStatus = Status.Patrolling;

                        }

                    }
                }

                break;

            case Status.Patrolling:
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
                //else
                //{
                //    enemyStatus = Status.Idle;
                //}

                break;

            case Status.Alerted:


            case Status.Engaging:

            default:
                enemyStatus = Status.Patrolling;
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
