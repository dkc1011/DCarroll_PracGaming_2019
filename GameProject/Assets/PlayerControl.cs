using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    /// <summary>
    /// The players movement speed
    /// </summary>
    private int playerSpeed;
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
    private int carrying;
    /// <summary>
    /// The amount of money the player  has, cannot be less than 0, there is no maximum amount
    /// </summary>
    private int money;
    /// <summary>
    /// The maximum amount of health the player can have
    /// </summary>
    private int maximumHealth;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// Player moves right relative to the camera at a given speed
    /// </summary>
    private void MoveRight(int playerSpeed)
    {
        throw new System.NotImplementedException();
    }

    /// <summary>
    /// Player moves left relative to the camera at a given speed
    /// </summary>
    private void MoveLeft(int playerSpeed)
    {
        throw new System.NotImplementedException();
    }

    /// <summary>
    /// Player moves away from the camera at a given speed
    /// </summary>
    private void MoveIn(int playerSpeed)
    {
        throw new System.NotImplementedException();
    }

    /// <summary>
    /// Player moves toward the camera at a given speed
    /// </summary>
    private void MoveOut(int playerSpeed)
    {
        throw new System.NotImplementedException();
    }

    /// <summary>
    /// Player is toggled into or out of crouch mode
    /// </summary>
    private void ToggleCrouch()
    {
        throw new System.NotImplementedException();
    }

    private void Shoot()
    {
        throw new System.NotImplementedException();
    }

    /// <summary>
    /// The player indicates that they would like to interact with an object. If there is an object, it checks what kind of object.
    /// </summary>
    private void Interact()
    {
        throw new System.NotImplementedException();
    }

    /// <summary>
    /// Method checks if the player is currently carrying something
    /// </summary>
    private bool isCarrying()
    {
        throw new System.NotImplementedException();
    }

    /// <summary>
    /// Method checks if the player or drone is currently active
    /// </summary>
    private void isActive()
    {
        throw new System.NotImplementedException();
    }

    /// <summary>
    /// Player throws the object it is carrying, if it is not carrying an object nothing is thrown
    /// </summary>
    private void Throw()
    {
        throw new System.NotImplementedException();
    }

    /// <summary>
    /// The player becomes inactive, the drone becomes active, if the player is inactive it becomes active and the drone becomes inactive
    /// </summary>
    internal void ToggleActive()
    {
        throw new System.NotImplementedException();
    }

    /// <summary>
    /// checks if the player is facing the same direction they are moving or not
    /// </summary>
    private bool ShouldTurn()
    {
        throw new System.NotImplementedException();
    }

    /// <summary>
    /// alters the player's facing direction to left
    /// </summary>
    private void TurnLeft()
    {
        throw new System.NotImplementedException();
    }

    /// <summary>
    /// alters the player's facing direction to face right
    /// </summary>
    private void TurnRight()
    {
        throw new System.NotImplementedException();
    }

    /// <summary>
    /// allows the player to ascend or descend regardless of gravity as long as they are on a ladder.
    /// </summary>
    private void ClimbLadder()
    {
        throw new System.NotImplementedException();
    }
}
