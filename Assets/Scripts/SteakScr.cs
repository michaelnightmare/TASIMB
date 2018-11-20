using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteakScr : MonoBehaviour
{

    public PlayerScript playerInteraction;
    




    // Use this for initialization
    void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            playerInteraction = other.GetComponent<PlayerScript>();
            consumeSteak();
            Debug.Log("health++");
            Destroy(gameObject);

        }
    }


    void consumeSteak()
    {
        playerInteraction.playerGetHealth();
    }       
    
}
