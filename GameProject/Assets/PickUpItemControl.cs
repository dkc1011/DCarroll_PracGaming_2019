using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpItemControl : MonoBehaviour {
    float turningSpeed = 50.0f;
    internal enum ItemType { Health, Money }
    PlayerControl myPlayer;


    //Declares what type of item this gameObject represents.
    internal ItemType thisItemIs;

    int valueOf;

    private void Start()
    {
        if(this.gameObject.CompareTag("Money"))
        {
            thisItemIs = ItemType.Money;
            valueOf = 1;
        }
        else if(this.gameObject.CompareTag("Health"))
        {
            thisItemIs = ItemType.Health;
            valueOf = 25;
        }

        myPlayer = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();
    }

    // Update is called once per frame
    void Update()
    {
        RotateLeft();


    }

    private void RotateLeft()
    {
        transform.Rotate(transform.up, turningSpeed * Time.deltaTime);
    }
    void OnTriggerEnter(Collider other)        
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (thisItemIs == ItemType.Money)
            {
                int newMoney = myPlayer.GetMoney() + valueOf;
                print("Money: " + newMoney);
                myPlayer.SetMoney(newMoney);
                this.gameObject.SetActive(false);
            }
            else if(thisItemIs == ItemType.Health)
            {
                int newHealth = myPlayer.GetHealth() + valueOf;
                print("Health: " + newHealth);
                myPlayer.SetHealth(newHealth);
                this.gameObject.SetActive(false);
            }
        }
    }
}
