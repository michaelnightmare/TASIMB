using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class GameManagerScr : MonoBehaviour {


    //Player
    public GameObject PlayerRef;
    public GameObject WinScene;
    public GameObject LoseScene;
    //Enemies
    public GameObject WolfRef;
    public GameObject EnemyCowboyRef;
    public GameObject BossEnemy;
    public GameObject RegEnemy;
    public int updatedDeathCounter; 
    public Text displayedText;
    public static GameManagerScr _instance;
    public GameObject[] enemies;
    public bool GameOverLose;
  
    
    bool test = false;
    
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
        LowPolyAnimalPack.WanderScript.AllAnimals.Clear();
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

        //SUPER TEMP
        PlayerRef.GetComponent<QuestManager>().IncrementKillQuest();

        if(updatedDeathCounter >=45)
        {
            YouWon();
        }
    }

    public void YouWon()
    {
        WinScene.SetActive(true);
        for(int i= 0; i > enemies.Length; i++)
        {
            BossEnemy.SetActive(false);
            RegEnemy.SetActive(false);
        }
    }
    public void YouLose()
    {
        LoseScene.SetActive(true);
    }
  
}
