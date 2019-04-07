using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGameManager : MonoBehaviour {

    public Button StartButton;
    public Button YesButton;
    public Button NoButton;
    public Button xTutorialButton;
    public static StartGameManager _instance;

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

    // Update is called once per frame
    void Update ()
    {
        //enemies = GameObject.FindGameObjectsWithTag("enemyCowboy");
        if (Input.GetButtonDown("StartButton"))
        {
            StartButton.onClick.Invoke();

        }
        if (Input.GetButtonDown("A"))
        {
            YesButton.onClick.Invoke();

        }
        if (Input.GetButtonDown("B"))
        {
            NoButton.onClick.Invoke();

        }
        if (Input.GetButtonDown("X"))
        {
            xTutorialButton.onClick.Invoke();

        }
    }
}
