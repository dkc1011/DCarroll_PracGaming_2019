using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryObjectControl : MonoBehaviour {
    PlayerControl myPlayer;
    Vector3 playerPosition;
    bool playerInRange;
    bool carried;
	// Use this for initialization
	void Start () {
        myPlayer = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();
        carried = false;

    }
	
	// Update is called once per frame
	void Update () {
        playerPosition = myPlayer.transform.position;

        if(myPlayer.IsPlayerActive())
        {
            if(Input.GetKeyDown("a"))
            {
                if(playerInRange)
                {
                    carried = true;
                }
            }
        }


        if (carried == true)
        {
            transform.position = new Vector3(playerPosition.x, playerPosition.y + 1, playerPosition.z);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {           
            playerInRange = true;
        }
        else
        {
            playerInRange = false;
        }
    }
}
