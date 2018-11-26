using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnScript : MonoBehaviour
{
    public GameObject PlayerRef;
    public Transform[] spawnPointsLocation;
    int spawnPointIndex;
    public GameObject spawnBox;
    public GameObject[] enemyType;
    public float tempEnemiesAcceleration;
    
    
  
    
   


    void Start()
    {
    
       
    }


    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer== 9)
        {
            if (spawnPointsLocation.Length == enemyType.Length)
            {
                for (int i = 0; i < spawnPointsLocation.Length; i++)
                {
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
       
        GameObject enemyTemp = Instantiate(enemyType, spawnLocation, Quaternion.identity) as GameObject;
        if (enemyTemp.GetComponent<AI_movement>())
        {
            enemyTemp.GetComponent<AI_movement>().EnemyAcceleration = tempEnemiesAcceleration;
            enemyTemp.GetComponent<AI_movement>().Player = PlayerRef.transform;
            

        }
        if(enemyTemp.GetComponent<AIWolf>())
          {
            enemyTemp.GetComponent<AIWolf>().Player = PlayerRef.transform;
            enemyTemp.GetComponent<AIWolf>().playerInteraction = PlayerRef.GetComponent<PlayerScript>();
        }

    }






}
