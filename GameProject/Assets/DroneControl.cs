using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneControl : MonoBehaviour {

    PlayerControl myPlayer;
    CameraControl droneCamera;
    Vector3 playerPosition;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	   /* if(IsDroneActive())
        {

        }
        else
        {
            transform.position = new Vector3(playerPosition.x, playerPosition.y + 2, playerPosition.z);
        } */
	}

    private bool IsDroneActive()
    {
        if(myPlayer.IsPlayerActive())
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    /*internal void getPlayerPosition(Vector3 position)
    {
        playerPosition = position;
    }*/
}
