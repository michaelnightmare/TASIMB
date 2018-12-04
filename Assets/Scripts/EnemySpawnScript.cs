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
    
    





    void Start()
    {
        


    }


    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer== 9 && spawned == false)
        {

            if (spawnPointsLocation.Length == enemyType.Length)
            {
                for (int i = 0; i < spawnPointsLocation.Length; i++)
                {
                    spawned = true;
                    spawnEnemies(spawnPointsLocation[spawnPointIndex + i].position, enemyType[i]);
                    
                    
                  
                    if (i == spawnPointsLocation.Length - 1)
                    {
                        
                        Destroy(spawnBox);
                    }

                }
            }
            else
            {
                Debug.Log("Spawn Error");
            }
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
