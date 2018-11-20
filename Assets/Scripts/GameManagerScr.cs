using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManagerScr : MonoBehaviour {


    //Player
    public GameObject PlayerRef;

    //Enemies
    public GameObject WolfRef;



    // Use this for initialization
    void Start () {
       

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
