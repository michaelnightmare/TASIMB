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
    public static GameManagerScr _instance;
    
    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this);
        }
    }


    // Use this for initialization
    void Start ()
    {
       

    }
	
	// Update is called once per frame
	void Update ()
    {
        

       
       
        
    }
   public void enemyCounterIncrease()
    {
        updatedDeathCounter++;
        displayedText.text = updatedDeathCounter.ToString();
    }

  
}
