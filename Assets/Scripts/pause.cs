using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pause : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        buttonpausegame();


    }

    public void pausegame()
    {
        if (Time.timeScale == 1.0f)
            Time.timeScale = 0.0f;
        else
            Time.timeScale = 1.0f;
    }


    public void buttonpausegame()
    {
        if (Input.GetKeyDown("escape"))
        {
            if (Time.timeScale == 1.0f)
                Time.timeScale = 0.0f;
            else
                Time.timeScale = 1.0f;
        }
    }
}
