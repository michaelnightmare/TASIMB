using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManagerScr : MonoBehaviour {


    //Player
    public GameObject PlayerRef;

    //Enemies
    public GameObject WolfRef;
    public Transform WolfSpawnLocation;



    // Use this for initialization
    void Start () {
        SpawnWolf(WolfSpawnLocation.position);

    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void SpawnWolf(Vector3 SpawnLocation)
    {
        GameObject WolfRefTemp = Instantiate(WolfRef, SpawnLocation, Quaternion.identity) as GameObject;
        WolfRefTemp.GetComponent<AIWolf>().Player = PlayerRef.transform;
        WolfRefTemp.GetComponent<AIWolf>().playerInteraction = PlayerRef.GetComponent<PlayerScript>();
    }
}
