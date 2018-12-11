using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventDialogScr : MonoBehaviour
{

    public DialogScr currentDialog;
    public GameObject dialogBoxDisplay;
    int dialogLine = 0;

    public Text displayedText;


    // Use this for initialization
    void Start ()
    {
       
    }
	
	// Update is called once per frame
	void Update ()
    {
      
        displayedText.text = currentDialog.dialog[dialogLine];
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {

            if (other.GetComponent<DialogScr>())
            {
                currentDialog = other.GetComponent<DialogScr>();
                Debug.Log("Triggered");
            }

        }

    }

}
