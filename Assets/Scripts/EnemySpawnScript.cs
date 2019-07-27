using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnScript : MonoBehaviour
{
    GameManagerScr GameManager;

    public GameObject PlayerRef;
    public Transform[] spawnPointsLocation;
    int spawnPointIndex;
    public GameObject spawnBox;
    public GameObject[] enemyType;
    public float tempEnemiesAcceleration;
    bool spawned = false;
    public int minSpawnable;
    public int maxSpawnable;
    private int spawnEnemyCount;

    public List<AIEnemy> enemyAis = new List<AIEnemy>();
    public List<AIWolf> alienAis = new List<AIWolf>();


    void Start()
    {
        spawnEnemyCount = Random.Range(minSpawnable, maxSpawnable);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer== 9 && spawned == false)
        {

            //if (spawnPointsLocation.Length == enemyType.Length)
            //{
                for (int i = 0; i < spawnEnemyCount; i++) //loop for the count we rolled on start
                {
                    int enemyRoll = Random.Range(0, enemyType.Length);
                    spawned = true;
                    spawnEnemies(spawnPointsLocation[spawnPointIndex + i].position, enemyType[enemyRoll]);
                    
                    if (i == spawnEnemyCount)
                    {
                        Destroy(spawnBox);
                    }
                }
           // }
            //else
           // {
           //     Debug.Log("Spawn Error");
           // }
       }
    }

    public void spawnEnemies(Vector3 spawnLocation, GameObject enemyType)
    {
        Vector3 playerDir = PlayerRef.transform.position - spawnLocation;
        playerDir.Scale(new Vector3(1, 0, 1));
        GameObject enemyTemp = Instantiate(enemyType, spawnLocation, Quaternion.LookRotation(playerDir)) as GameObject;

        if (enemyTemp.GetComponent<AIEnemy>())
        {
            AIEnemy ai = enemyTemp.GetComponent<AIEnemy>();
            enemyAis.Add(ai);
            ai.OnEnemyDeath.AddListener(delegate { EnemyDied(ai); });
            ai.target = PlayerRef.transform;
        }

        if(enemyTemp.GetComponent<AIWolf>())
        {
            AIWolf wolf = enemyTemp.GetComponent<AIWolf>();
            alienAis.Add(wolf);
            wolf.OnEnemyDeath.AddListener(delegate { WolfDied(wolf); });
            wolf.target = PlayerRef.transform;
            wolf.playerInteraction = PlayerRef.GetComponent<PlayerScript>();
        }

    }

    public void KillRemainingEnemies()
    {
        foreach(AIEnemy ai in enemyAis)
        {
            Destroy(ai.gameObject);
        }

        foreach (AIWolf ai in alienAis)
        {
            Destroy(ai.gameObject);
        }

        alienAis.Clear();
        enemyAis.Clear();
    }

    public void EnemyDied(AIEnemy enemy)
    {
        if (enemyAis.Contains(enemy)) enemyAis.Remove(enemy);
        CheckAllDead();
    }

    public void WolfDied(AIWolf wolf)
    {
        if (alienAis.Contains(wolf)) alienAis.Remove(wolf);
        CheckAllDead();
    }

    void CheckAllDead()
    {
        if (alienAis.Count == 0 && enemyAis.Count == 0)
        {
            QuestSystem.quests.NotifyKill(gameObject);
        }
    }
}
