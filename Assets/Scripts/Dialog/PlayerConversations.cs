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

    public AudioClip currentSoundClip;
    public AudioSource NPCSounds;

  


    // Use this for initialization
    void Start ()
    {
        NPCSounds = GetComponent<AudioSource>();

    }

    public void InitiateDialog(DialogScr D)
    {
        currentDialog = D;
        currentSoundClip = currentDialog.howdyClip;
        dialogLine = 0;
        dialogBoxDisplay.SetActive(true);
        displayedText.text = currentDialog.dialog[dialogLine];
        NPCSounds.clip = currentSoundClip;
        NPCSounds.Play();
        dialogLine++;
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
                    currentDialog.dialogFinished.Invoke();
                    closeDialog();

                 
                }
                else 
                {
                    dialogBoxDisplay.SetActive(true);
                    displayedText.text = currentDialog.dialog[dialogLine];
                    
                    if(dialogLine <= 0)
                    {
                        NPCSounds.clip = currentSoundClip;
                        NPCSounds.Play();
                        currentDialog.dialogStarted.Invoke();
                        dialogLine++;
                    }
                    else
                    {
                        dialogLine++;
                    }

                }

            }
            if (Input.GetButtonDown("StopConversation"))
            {
                closeDialog();
                
            }
        }
	}

   public void closeDialog()
    {
        dialogBoxDisplay.SetActive(false);
        dialogLine = 0;
        if (currentDialog.disableDialogOnceComplete)
        {
            currentDialog = null;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 17)
        {

            if (other.GetComponent<DialogScr>())
            {
                currentDialog = other.GetComponent<DialogScr>();
                currentSoundClip = currentDialog.howdyClip;
              
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
                dialogBoxDisplay.SetActive(false);
                dialogLine = 0;
                currentDialog = null;
            }

        }

    }
}
