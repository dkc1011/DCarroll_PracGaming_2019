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

    private float time = 0;

    private int weaponDamage;

    private bool IJustHit;

    Vector3 raycastLocation;

    // Use this for initialization
    void Start () {
        ourCamera = Camera.main.GetComponent<CameraControl>();
        myDrone = GameObject.FindWithTag("Drone").GetComponent<DroneControl>();
        active = true;
        playerSpeed = 2.4f;
        facing = 'r';
        weaponDamage = 25;
        IJustHit = false;

        //Start player on ground
        /*Vector3 dwn = Vector3.down;
        Debug.DrawRay(transform.position, dwn * 0.01f, Color.white, 1);
        RaycastHit info;
        if (Physics.Raycast(transform.position, dwn * 0.001f, out info, 1))
        {
            isGrounded = true;
            transform.position = info.point + 0.5f * Vector3.up;
        }*/
    }

    private void Awake()
    {
        velocity = new Vector3(0, 7, 0);
        acceleration = new Vector3(0, -9, 0);
        gravity = new Vector3(0, -14f, 0);
    }

    // Update is called once per frame
    void Update() {
       

        //Checks if the player character is currently active
        if (IsPlayerActive())
        {
 

            //Various Movement Triggers
            if (ShouldMoveRight())
            {
                Move(playerSpeed);
            }

            if (ShouldMoveLeft())
            {
                Move(playerSpeed);
            }

            if (ShouldMoveIn())
            {
                if (transform.position.x >= -3.5f)
                    Move(playerSpeed);
            }

            if (ShouldMoveOut())
            {
                if (transform.position.x <= 2.5f)
                    Move(playerSpeed);
            }

            //Toggle Crouching Trigger
            if (Input.GetKeyDown("c"))
            {
                ToggleCrouch();
            }

            //Shooting Trigger
            if (Input.GetKeyDown("z"))
            {
                Shoot();
            }

            //Jumping
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        } //End is Active

        //Checks every 1/3 of a second if the player is touching the ground

        Vector3 dwn = Vector3.down;
        
        time += Time.deltaTime;

        if (time >= 0.333333)
        {

            if (Physics.CheckBox(transform.position + 0.6f * Vector3.down, new Vector3(0.4f, 0.05f, 0.4f)))

            {
                isGrounded = true;
                //  transform.position = info.point + 0.5f * Vector3.up;
            }

            else
            {
                isGrounded = false;
                Airbourne = true;
            }

            time = 0;
        }

        //Checks if the player is Airbourne

        if (Airbourne)
        {
            velocity += gravity* Time.deltaTime;
            transform.position += velocity * Time.deltaTime;
            if (velocity.y < 0)
            {
               
                if (Physics.CheckBox(transform.position + 0.59f * Vector3.down, new Vector3(0.4f, 0.05f, 0.4f)))

                {
                    isGrounded = true;
                   //transform.position = Physics.CheckBox(transform.position + 0.59f * Vector3.down, new Vector3(0. + 0.5f * Vector3.up;
                }

                else
                {
                    isGrounded = false; 

                }//if (Physics.Raycast(transform.position, dwn * 1, out info, 1))
                //{
                //    isGrounded = true;
                //    transform.position = info.point + 0.5f * Vector3.up;
                //}
                //else
                //{
                //    isGrounded = false;
                //}
            }
        }//End if(Airbourne)


        if (isGrounded && Airbourne)
        {
            velocity = Vector3.zero;
            Airbourne = false;
        }

        //ToggleActive trigger

        if (Input.GetKeyDown("d"))
        {
            ToggleActive();
        }//End Keybind

        ourCamera.PlayerPositionIs(transform.position);

    }//End Update

    /// <summary>
    /// Player moves right relative to the camera at a given speed
    /// </summary>
    private void Move(float playerSpeed)
    {
        transform.position += playerSpeed * transform.forward * Time.deltaTime;
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
            playerSpeed = 2.8f;
        }
        else
        {
            crouched = true;
            playerSpeed = 1.4f;
        }
    }//End ToggleCrouch()

    private void Shoot()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, fwd, 10))
        {
            print("Hit with Bullet");
            IJustHit = true;
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

    internal bool GetEnemyHit()
    {
        return IJustHit;
    }

    internal void SetEnemyHit(int result)
    {
        if (result == 1)
        {
            IJustHit = false;
        }
        else
        {
            IJustHit = true;
        }
    }
}
