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
           enemyTemp.GetComponent<AIEnemy>().target = PlayerRef.transform;
        }

        if(enemyTemp.GetComponent<AIWolf>())
        {
           enemyTemp.GetComponent<AIWolf>().target = PlayerRef.transform;
           enemyTemp.GetComponent<AIWolf>().playerInteraction = PlayerRef.GetComponent<PlayerScript>();
        }

    }
}
