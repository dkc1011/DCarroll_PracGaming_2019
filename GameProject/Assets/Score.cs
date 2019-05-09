using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {
    PlayerControl myPlayer;
    public Text scoreText;

    // Use this for initialization
    void Start () {
        myPlayer = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();
    }
	
	// Update is called once per frame
	void Update () {
        scoreText.text = myPlayer.GetMoney().ToString();
    }
}
