using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnScript : MonoBehaviour
{
    public GameObject PlayerRef;
    public GameObject enemyCowboy;
    public Transform[] spawnPointsLocation;
    int spawnPointIndex;
    public GameObject spawnBox;
    GameObject enemyCowboyTemp;
    



    void Start()
    {
        
    }


    void OnTriggerEnter(Collider other)
    {
        
            
            for (int i = 0; i < spawnPointsLocation.Length; i++)
            {
                spawnEnemies(spawnPointsLocation[spawnPointIndex + i].position);
            if(i == 2)
            {
              
                Destroy(spawnBox);
            }
           
            }
           
       
    }

    public void spawnEnemies(Vector3 spawnLocation)
    {

        GameObject enemyCowboyTemp = Instantiate(enemyCowboy, spawnLocation, Quaternion.identity) as GameObject;
        enemyCowboyTemp.GetComponent<AI_movement>().Player = PlayerRef.transform;
        
       
        
       
    }
}
