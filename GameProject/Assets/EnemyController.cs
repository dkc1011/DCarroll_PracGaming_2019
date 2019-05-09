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
    private Vector3 fwd;
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
        enemySpeed = 2.5f;
        
    }
	
	// Update is called once per frame
	void Update () {
        if(health <= 0)
        {
            this.gameObject.SetActive(false);
        }


        switch (enemyStatus)
        {
            case Status.Idle:

                waitTimer += Time.deltaTime;

                if (waitTimer >= 3 && waitTimer <= 6)
                {
                        FaceOut();
                        fwd = transform.forward;

                        CheckPlayerSpotted();

                        if (WasPlayerSpotted())
                        {
                            enemyStatus = Status.Patrolling;
                        }
                }
                else if (waitTimer > 6 && waitTimer <= 9)
                {
                        FaceRight();
                        fwd = transform.forward;

                        CheckPlayerSpotted();

                    if (WasPlayerSpotted())
                    {
                        enemyStatus = Status.Patrolling;
                    }
                }
                    else if (waitTimer > 9 && waitTimer <= 12)
                    {
                        FaceIn();
                        fwd = transform.forward;

                        CheckPlayerSpotted();

                    if (WasPlayerSpotted())
                    {
                        enemyStatus = Status.Patrolling;
                    }
                }
                    else if (waitTimer > 12)
                    {
                        waitTimer = 0;
                        FaceLeft();
                        fwd = transform.forward;

                        CheckPlayerSpotted();

                    if (WasPlayerSpotted())
                    {
                        enemyStatus = Status.Patrolling;
                    }
                }                    
                    

                break;

            case Status.Patrolling:
                if(targetPosition != transform.position)
                {
                    waitTimer += Time.deltaTime;

                    if (targetPosition.z <= transform.position.z)
                    {
                        PatrolRight();

                        if (waitTimer >= 3)
                        {
                            if(WasPlayerSpotted())
                            {
                                enemyStatus = Status.Engaging;
                            }
                            else
                            {
                                enemyStatus = Status.Idle;
                            }

                            waitTimer = 0;
                        }
                        
                    }
                    else if(targetPosition.z >= transform.position.z)
                    {
                        PatrolLeft();

                        if (waitTimer >= 3)
                        {
                            if (WasPlayerSpotted())
                            {
                                enemyStatus = Status.Engaging;
                            }
                            else
                            {
                                enemyStatus = Status.Idle;
                            }

                            waitTimer = 0;
                        }
                    }
                    else if(targetPosition.x >= transform.position.x)
                    {
                        PatrolUp();

                        if (waitTimer >= 3)
                        {
                            if (WasPlayerSpotted())
                            {
                                enemyStatus = Status.Engaging;
                            }
                            else
                            {
                                enemyStatus = Status.Idle;
                            }
                        }
                    }
                    else if(targetPosition.x <= transform.position.x)
                    {
                        PatrolDown();

                        if (waitTimer >= 3)
                        {
                            if (WasPlayerSpotted())
                            {
                                enemyStatus = Status.Engaging;
                            }
                            else
                            {
                                enemyStatus = Status.Idle;
                            }
                        }
                    }

                }
                else
                {
                    enemyStatus = Status.Idle;
                }

                break;

            case Status.Engaging:
                if (WasPlayerSpotted())
                {
                    Debug.Log("Enemy : Engaging");

                    waitTimer += Time.deltaTime;

                    if(waitTimer >= 1)
                    {
                        myPlayer.SetHealth(myPlayer.GetHealth() - 15);
                        waitTimer = 0;
                    }
                }
                else
                {
                    enemyStatus = Status.Patrolling;
                }
                break;

            default:
                enemyStatus = Status.Patrolling;
                break;
        }
	}
    private void Move(float enemySpeed)
    {
        transform.position += enemySpeed * transform.forward * Time.deltaTime;
    }//End Move()


    private void Die()
    {

    }

    private void PatrolLeft()
    {
        FaceLeft();
        Debug.Log("Enemy : Patrolling left");
        Move(enemySpeed);
    }

    private void PatrolRight()
    {
        FaceRight();
        Debug.Log("Enemy : Patrolling right");
        Move(enemySpeed);
    }

    private void PatrolUp()
    {
        FaceIn();
        Debug.Log("Enemy : Patrolling In");
        Move(enemySpeed);
    }

    private void PatrolDown()
    {
        FaceOut();
        Debug.Log("Enemy : Patrolling Out");
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

    private void CheckPlayerSpotted()
    {
        Debug.DrawRay(transform.position, fwd, Color.white, 3);
        if (Physics.Raycast(transform.position, fwd, out playerCheck, 15))
        {
            if (playerCheck.collider.tag == "Player")
            {
                Debug.Log("Enemy : Player spotted");

                if (playerCheck.point.z < transform.position.z)
                {
                    targetPosition = new Vector3(myPlayer.transform.position.x, myPlayer.transform.position.y, myPlayer.transform.position.z);
                }
                else if (playerCheck.point.z > transform.position.z)
                {
                    targetPosition = new Vector3(myPlayer.transform.position.x, myPlayer.transform.position.y, myPlayer.transform.position.z);
                }

            }

        }
    }

    private bool WasPlayerSpotted()
    {
        if (Physics.Raycast(transform.position, fwd, out playerCheck, 15))
        {
            if (playerCheck.collider.tag == "Player")
            {
                return true;

            }

        }

        return false;
    }

    public void TakeDamage(int Damage)
    {
        health -= 25;
    }

}
