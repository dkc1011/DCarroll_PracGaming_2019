using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    //------------------------------------------------------------------//
    //                          PlayerControl.cs                        //
    //             Player Movement, Actions and Controlls               //
    //                                                                  //
    //------------------------------------------------------------------//

    Animator myAnim;
    CameraControl ourCamera;
    DroneControl myDrone;
    /// <summary>
    /// The players movement speed
    /// </summary>
    private float playerSpeed;
    /// <summary>
    /// the player is currently active or inactive
    /// </summary>
    private bool active;
    /// <summary>
    /// The amount of health the player has, cannot be less than 0, cannot be more than maximumHealth
    /// </summary>
    private int health;
    /// <summary>
    /// the player is or is not currently crouched
    /// </summary>
    private bool crouched;
    /// <summary>
    /// the player is or is not carrying an object
    /// </summary>
    private bool carrying;
    /// <summary>
    /// The amount of money the player  has, cannot be less than 0, there is no maximum amount
    /// </summary>
    private int money;
    /// <summary>
    /// The maximum amount of health the player can have
    /// </summary>
    private int maximumHealth;

    internal char facing;

    private Vector3 velocity, acceleration, gravity;

    private bool isGrounded = true, Airbourne = false;

    private float jumpForce = 9f;

    private float timeGroundCheck = 0;

    private int weaponDamage;

    private bool IJustHit;

    public GameObject DronePrefab;

    public GameObject Drone;

    private Rigidbody rb;

    Vector3 raycastLocation;

    // Use this for initialization
    void Start () {
        //Attaches the player object to it's animator and Camera
        myAnim = GetComponent<Animator>();
        ourCamera = Camera.main.GetComponent<CameraControl>();
        rb = GetComponent<Rigidbody>();

        //Initializes various variables
        health = 100;
        active = true;
        playerSpeed = 3.9f;
        facing = 'r';
        weaponDamage = 25;
        IJustHit = false;

        //Instantiates the Drone
        Drone = (GameObject)Instantiate(DronePrefab, new Vector3(transform.position.x - 2, transform.position.y + 1, transform.position.z - 3), Quaternion.identity);
        SetMyDrone(GameObject.FindWithTag("Drone").GetComponent<DroneControl>());
    }

    private void Awake()
    {
        velocity = new Vector3(0, 7, 0);
        acceleration = new Vector3(0, -9, 0);
        gravity = new Vector3(0, -14f, 0);
    }

    // Update is called once per frame
    void Update() {
        if (health <= 0)
        {
            this.gameObject.SetActive(false);
        }


        //Checks if the player character is currently active
        if (IsPlayerActive())
        {
            //ToggleCrouching trigger
            if (Input.GetKeyDown("c"))
            {
                ToggleCrouch();
            }

            //Checks every tick if the player has pressed a movement key
            CheckMovement();

           
            //Shooting Trigger
            if (Input.GetKeyDown("z"))
            {
                Shoot();
                myAnim.SetTrigger("Shoot");
            }


            //Jumping
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (!crouched)
                {
                    Jump();
                    myAnim.SetTrigger("Jump");
                }
            }
        } //End is Active


        //GroundCheck and Jumping

        //Checks every 1/3 of a second if the player is touching the ground
        
        Vector3 dwn = Vector3.down;
        
        timeGroundCheck += Time.deltaTime;

        if (timeGroundCheck >= 0.333333)
        {

            if (Physics.CheckBox(transform.position + 0.1f * Vector3.down, new Vector3(0.4f, 0.05f, 0.2f)))

            {
                isGrounded = true;
                //  transform.position = info.point + 0.5f * Vector3.up;
            }

            else
            {
                isGrounded = false;
                Airbourne = true;
            }

            timeGroundCheck = 0;
        }
        

        //Checks if the player is Airbourne

        if (Airbourne)
        {
            velocity += gravity* Time.deltaTime;
            transform.position += velocity * Time.deltaTime;
            if (velocity.y < 0)
            {
               
                if (Physics.CheckBox(transform.position + 0.2f * Vector3.down, new Vector3(0.4f, 0.0001f, 0.2f)))

                {
                    isGrounded = true;
                   //transform.position = Physics.CheckBox(transform.position + 0.59f * Vector3.down, new Vector3(0. + 0.5f * Vector3.up;
                }

                else
                {
                    isGrounded = false;
                    transform.position += velocity * Time.deltaTime;
                }
            }
        }//End if(Airbourne)


        if (isGrounded && Airbourne)
        {
            velocity = Vector3.zero;
            Airbourne = false;
        }



        //ToggleActive trigger -- When the player pressed D, the Drone becomes active

        if (Input.GetKeyDown("d"))
        {
            ToggleActive();
        }//End Keybind

        ourCamera.PlayerPositionIs(transform.position);

    }//End Update


    private bool CheckWallCollision()
    {     
         if (Physics.CheckBox(transform.position + 0.4f * Vector3.forward, new Vector3(0.5f, 0.01f, 0.3f)))
         {
              return true;
         }
         else
         {
              return false;
         }       
    }

    private void CheckMovement()
    {
        if (ShouldMoveRight())
        {
            //if (!CheckWallCollision())
            {
                Move(playerSpeed);
            }
        }


        if (ShouldMoveLeft())
        {
            //if (!CheckWallCollision())
            {
                Move(playerSpeed);
            }
        }


        if (ShouldMoveIn())
        {

            if (transform.position.x >= -3.5f)
            {
                //if (!CheckWallCollision())
                {
                    Move(playerSpeed);
                }
            }
        }

        if (ShouldMoveOut())
        {
            if (transform.position.x <= 3.5f)
            {
                //if (!CheckWallCollision())
                {
                    Move(playerSpeed);
                }
            }
        }

        if (!ShouldMoveIn() && !ShouldMoveOut() && !ShouldMoveRight() && !ShouldMoveLeft())
        {
            myAnim.SetBool("Moving", false);
        }
    }



    private void Move(float playerSpeed)
    {
        transform.position += playerSpeed * transform.forward * Time.deltaTime;
        myAnim.SetBool("Moving", true);
    }//End Move()



    private bool CanJump()
    {
        return !Airbourne;
    }//End CanJump()



    private void Jump()
    {

        if (CanJump())
        {
            //Upward code
            Airbourne = true;
            isGrounded = false;
            velocity += Vector3.up * jumpForce;
        }

    }//End Jump()



    private bool ShouldMoveRight()
    {
        if(Input.GetKey("right"))
        {
            if(facing != 'r')
            {
                if(facing == 'u')
                {
                    transform.Rotate(Vector3.up, 90);
                    facing = 'r';
                }
                else if(facing == 'd')
                {
                    transform.Rotate(Vector3.up, 270);
                    facing = 'r';
                }
                else if(facing == 'l')
                {
                    transform.Rotate(Vector3.up, 180);
                    facing = 'r';
                }
            }
            else
            {
                facing = 'r';
            }//End If/Else

            return true;
        }
        else
        {
            return false;
        }//End If/Else
    }//End ShouldMoveRight()



    /// <summary>
    /// Player moves left relative to the camera at a given speed
    /// </summary>
    private bool ShouldMoveLeft()
    {
        if (Input.GetKey("left"))
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

            return true;
        }
        else
        {
            return false;
        }//End If/Else
    }//End ShouldMoveLeft



    /// <summary>
    /// Player moves away from the camera at a given speed
    /// </summary>
    private bool ShouldMoveIn()
    {
        if (Input.GetKey("up"))
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

            return true;
        }
        else
        {
            return false;
        }//End If/Else
    }//End ShouldMoveIn()



    /// <summary>
    /// Player moves toward the camera at a given speed
    /// </summary>
    private bool ShouldMoveOut()
    {
        if (Input.GetKey("down"))
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

            return true;
        }
        else
        {
            return false;
        }//End If/Else
    }//End ShouldMoveOut()



    /// <summary>
    /// Player is toggled into or out of crouch mode
    /// </summary>
    private void ToggleCrouch()
    {
        if (crouched)
        {
            crouched = false;
            myAnim.SetBool("Crouching", false);
            playerSpeed = 3.9f;
        }
        else
        {
            crouched = true;
            myAnim.SetBool("Crouching", true);
            playerSpeed = 2.5f;
        }
    }//End ToggleCrouch()



    private void Shoot()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        RaycastHit enemyHit;

        if (Physics.Raycast(transform.position, fwd, out enemyHit, 10))
        {
            EnemyController enemyHealth;

            if (enemyHit.collider.tag == "Enemy")
            {
                print("Hit Enemy with Bullet");
                enemyHealth = enemyHit.collider.GetComponent<EnemyController>();
                enemyHealth.TakeDamage(weaponDamage);
            }
            else
            {
                print("Hit something else with Bullet");
            }
        }
        else
        {
            print("No hit");
        }

        IJustHit = false;
    }//End Shoot()


    /// <summary>
    /// The player indicates that they would like to interact with an object. If there is an object, it checks what kind of object.
    /// </summary>
    private void Interact()
    {
        throw new System.NotImplementedException();
    }//End Interact()


    /// <summary>
    /// Method checks if the player is currently carrying something
    /// </summary>
    internal bool IsCarrying()
    {
        if(carrying)
        {
            return true;
        }
        else
        {
            return false;
        }
    }//End IsCarrying()



    internal void ToggleCarry()
    {
        if(carrying)
        {
            carrying = false;
        }
        else
        {
            carrying = true;
        }
    }//End ToggleCarry()



    /// <summary>
    /// Method checks if the player or drone is currently active
    /// </summary>
    internal bool IsPlayerActive()
    {
        if(active == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }//End IsPlayerActive()



    /// <summary>
    /// Player throws the object it is carrying, if it is not carrying an object nothing is thrown
    /// </summary>
    private void Throw()
    {
        throw new System.NotImplementedException();
    }//End Throw()



    /// <summary>
    /// The player becomes inactive, the drone becomes active, if the player is inactive it becomes active and the drone becomes inactive
    /// </summary>
    internal void ToggleActive()
    {
        if(active == true)
        {
            active = false;
            print("Drone active");
        }
        else
        {
            active = true;
            print("Player active");
        }
    }//End ToggleActive()



    /// <summary>
    /// allows the player to ascend or descend regardless of gravity as long as they are on a ladder.
    /// </summary>
    private void ClimbLadder()
    {
        throw new System.NotImplementedException();
    }//End ClimbLadder()



    private void OnTriggerEnter(Collider other)
    {

    }//End OnTriggerEnter()



    internal void SetMoney(int m)
    {
        money = m;
    }



    internal int GetMoney()
    {
        return money;
    }



    internal void SetHealth(int h)
    {
        health = h;
        if(health >= 100)
        {
            health = 100;
        }
    }



    internal int GetHealth()
    {
        return health;
    }



    internal char IAmFacing()
    {
        return facing;
    }



    internal int GetWeaponDamage()
    {
        return weaponDamage;
    }

    private void SetMyDrone(DroneControl myDrone)
    {
        myDrone = this.myDrone;
    }

    public DroneControl GetMyDrone()
    {
        return myDrone;
    }





}
