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

    public GameObject minimap;
    bool isMapOn;


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
        isMapOn = true;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            isMapOn = !isMapOn;
        }

        if (isMapOn)
        {
            minimap.SetActive(true);
        }
        else
        {
            minimap.SetActive(false);
        }

    }

    public void enemyCounterIncrease()
    {
        updatedDeathCounter++;
        displayedText.text = updatedDeathCounter.ToString();

        //SUPER TEMP, should increment this differently
        PlayerRef.GetComponent<QuestManager>().IncrementKillQuest();
    }

    public void YouWon()
    {
        WinScene.SetActive(true);

        if (isMapOn)
        {
            isMapOn = false;
        }

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
