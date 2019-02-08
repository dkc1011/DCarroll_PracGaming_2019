using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {
    float heightOfCamera = 3;
    float Xpos = 9;
    Vector3 playerPosition;
    Vector3 dronePosition;
    PlayerControl myPlayer;
    DroneControl myDrone;

    // Use this for initialization
    void Start () {
        myPlayer = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();
        myDrone = GameObject.FindWithTag("Drone").GetComponent<DroneControl>();
    }
	
	// Update is called once per frame
	void Update () {
        if(myPlayer.IsPlayerActive())
        {
            followPlayer(playerPosition);
        }
        else
        {
            followDrone(dronePosition);
        }
	}

    internal void followPlayer(Vector3 position)
    {
        transform.position = new Vector3(Xpos, position.y + heightOfCamera, position.z);
    }

    internal void followDrone(Vector3 position)
    {
        transform.position = new Vector3(Xpos, position.y + heightOfCamera, position.z);
    }

    internal void playerPositionIs(Vector3 position)
    {
        //transform.position = new Vector3(Xpos, position.y + heightOfCamera, position.z);
        playerPosition = position;
    }

    internal void dronePositionIs(Vector3 position)
    {
        dronePosition = position;
    }
}
