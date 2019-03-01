using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {
    float heightOfCamera = 3;
    float Xpos = 9;
    Vector3 targetPosition;
    Vector3 playerPosition;
    Vector3 dronePosition;
    PlayerControl myPlayer;
    DroneControl myDrone;

    // Use this for initialization
    void Start () {
        myPlayer = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();
        myDrone = GameObject.FindWithTag("Drone").GetComponent<DroneControl>();
        transform.position = new Vector3(Xpos, myPlayer.transform.position.y + heightOfCamera, myPlayer.transform.position.z);
    }
	
	// Update is called once per frame
	void Update () {
        if(myPlayer.IsPlayerActive())
        {
            FollowPlayer(playerPosition);
        }
        else
        {
            FollowDrone(dronePosition);
        }

        transform.position = Vector3.Lerp(transform.position, targetPosition, 0.1f);
    }

    internal void FollowPlayer(Vector3 position)
    {
        targetPosition = new Vector3(Xpos, myPlayer.transform.position.y + heightOfCamera, myPlayer.transform.position.z);
    }

    internal void FollowDrone(Vector3 position)
    {
        targetPosition = new Vector3(Xpos, myDrone.transform.position.y, myDrone.transform.position.z);
    }

    internal void PlayerPositionIs(Vector3 position)
    {
        //transform.position = new Vector3(Xpos, position.y + heightOfCamera, position.z);
        playerPosition = position;
    }

    internal void DronePositionIs(Vector3 position)
    {
        dronePosition = position;
    }
}
