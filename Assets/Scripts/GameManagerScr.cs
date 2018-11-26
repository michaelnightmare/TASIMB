using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManagerScr : MonoBehaviour {


    //Player
    public GameObject PlayerRef;

    //Enemies
    public GameObject WolfRef;
    public GameObject EnemyCowboyRef;
    public int updatedDeathCounter; 
    public Text displayedText;
    public GameObject[] enemiesIngame;



    // Use this for initialization
    void Start ()
    {
       

    }
	
	// Update is called once per frame
	void Update ()
    {
        
        updatedDeathCounter += WolfRef.GetComponent<AIWolf>().enemyWolfDeathCounter + EnemyCowboyRef.GetComponent<AI_movement>().enemyDeathCounter;
       
        displayedText.text = updatedDeathCounter.ToString();

    }

  
}
