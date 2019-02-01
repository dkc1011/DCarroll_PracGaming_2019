using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {
    float heightOfCamera = 3;
    float Xpos = 9;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    internal void playerPositionIs(Vector3 position)
    {
        transform.position = new Vector3(Xpos, position.y + heightOfCamera, position.z);
    }
}
