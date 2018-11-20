using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerConversations : MonoBehaviour
{
   public DialogScr currentDialog;
    public GameObject dialogBoxDisplay;
    int dialogLine = 0;
    public Text displayedText;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		  if(currentDialog != null)
        {
            if (Input.GetButtonDown("Talk"))
            {
                if (dialogLine > currentDialog.dialog.Count-1)
                {
                    closeDialog();
                }
                else
                {
                    dialogBoxDisplay.SetActive(true);
                    displayedText.text = currentDialog.dialog[dialogLine];
                    dialogLine++;
                }

                
                
            }
            if(Input.GetButtonDown("StopConversation"))
            {
                closeDialog();
                
            }
        }
	}

    void closeDialog()
    {
        dialogBoxDisplay.SetActive(false);
        dialogLine = 0;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 17)
        {

            if (other.GetComponent<DialogScr>())
            {
                currentDialog = other.GetComponent<DialogScr>();
                currentDialog.showOverhead();

            }

        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 17)
        {

            if (other.GetComponent<DialogScr>())
            {

                currentDialog.hideOverhead();
                currentDialog = null;
            }

        }

    }
}
