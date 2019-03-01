using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItemControl : MonoBehaviour {
    float turningSpeed = 50.0f;
    internal enum itemType { Health, Money }
    PlayerControl myPlayer;

    //Declares what type of item this gameObject represents.
    internal itemType thisItemIs;

    int valueOf;

    private void Start()
    {
        if(this.gameObject.CompareTag("Money"))
        {
            thisItemIs = itemType.Money;
            valueOf = 1;
        }
        else if(this.gameObject.CompareTag("Health"))
        {
            thisItemIs = itemType.Health;
            valueOf = 25;
        }

        myPlayer = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();
    }

    // Update is called once per frame
    void Update()
    {
        rotateLeft();
    }

    private void rotateLeft()
    {
        transform.Rotate(transform.up, turningSpeed * Time.deltaTime);
    }
    void OnTriggerEnter(Collider other)        
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (thisItemIs == itemType.Money)
            {
                int newMoney = myPlayer.GetMoney() + valueOf;
                print("Money: " + newMoney);
                myPlayer.SetMoney(newMoney);
                this.gameObject.SetActive(false);
            }
            else if(thisItemIs == itemType.Health)
            {
                int newHealth = myPlayer.GetHealth() + valueOf;
                print("Health: " + newHealth);
                myPlayer.SetHealth(newHealth);
                this.gameObject.SetActive(false);
            }
        }
    }
}
