using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneControl : MonoBehaviour {
    //------------------------------------------------------------------//
    //                          DroneControl.cs                         //
    //             Drone Movement, Actions and Controlls                //
    //                                                                  //
    //------------------------------------------------------------------//


    PlayerControl myPlayer;
    CameraControl droneCamera;
    Vector3 playerPosition;
    private float droneSpeed;
    private char facing = 'r';
    Vector3 targetPosition;
    private Quaternion targetOrientation;
    private bool holdingPosition;

    // Use this for initialization
    void Start () {
        myPlayer = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();
        droneSpeed = 3.4f;
        droneCamera = Camera.main.GetComponent<CameraControl>();
    }
	
	// Update is called once per frame
	void Update () {


        if (myPlayer.IsPlayerActive())
        {
            if(!IsHoldingPosition())
            {
                targetOrientation = myPlayer.transform.rotation;

                if (myPlayer.facing == 'r')
                {
                    targetPosition = new Vector3(myPlayer.transform.position.x, myPlayer.transform.position.y + 1, myPlayer.transform.position.z - 1.5f);

                    facing = 'r';
                }
                else if (myPlayer.facing == 'l')
                {
                    targetPosition = new Vector3(myPlayer.transform.position.x, myPlayer.transform.position.y + 1, myPlayer.transform.position.z + 1.5f);
                    facing = 'l';
                }
                else if (myPlayer.facing == 'd')
                {
                    targetPosition = new Vector3(myPlayer.transform.position.x - 1, myPlayer.transform.position.y + 1, myPlayer.transform.position.z + 1.5f);
                    facing = 'd';
                }
                else
                {
                    targetPosition = new Vector3(myPlayer.transform.position.x + 1, myPlayer.transform.position.y + 1, myPlayer.transform.position.z - 1.5f);
                    facing = 'u';
                }

                transform.position = Vector3.Lerp(transform.position, targetPosition, 0.025f);

                transform.rotation = Quaternion.Slerp(transform.rotation, targetOrientation, 0.5f);
            }

        }
        else
        {
            droneCamera.DronePositionIs(transform.position);

            //Various Movement Triggers
            if (ShouldMoveRight())
            {
                Move(droneSpeed);
            }

            if (ShouldMoveLeft())
            {
                Move(droneSpeed);
            }

            if (ShouldMoveIn())
            {
                if (transform.position.x >= -2.5)
                    Move(droneSpeed);
            }

            if (ShouldMoveOut())
            {
                if (transform.position.x <= 2.5)
                    Move(droneSpeed);
            }

            if (Input.GetKey("space"))
            {
                Ascend(droneSpeed);
            }

            if (Input.GetKey("c"))
            {
                Descend(droneSpeed);
            }
            if (Input.GetKeyDown("x"))
            {
                ToggleHoldPosition();
            }
        }
            
	}

    private void Move(float droneSpeed)
    {
        transform.position += droneSpeed * transform.forward * Time.deltaTime;
    }

    private void Ascend(float droneSpeed)
    {
        transform.position += droneSpeed * transform.up * Time.deltaTime;
    }

    private void Descend(float droneSpeed)
    {
        transform.position -= droneSpeed * transform.up * Time.deltaTime;
    }

    private bool ShouldMoveRight()
    {
        if (Input.GetKey("right"))
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
            }

            return true;
        }
        else
        {
            return false;
        }
    }

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
            }

            return true;
        }
        else
        {
            return false;
        }
    }

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
            }

            return true;
        }
        else
        {
            return false;
        }
    }

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
        }
    }

    internal bool IsHoldingPosition()
    {
        if(holdingPosition == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void ToggleHoldPosition()
    {
        if(holdingPosition == true)
        {
            holdingPosition = false;
            print("Drone follows player");
        }
        else
        {
            holdingPosition = true;
            print("Drone holds position");
        }
    }
}
