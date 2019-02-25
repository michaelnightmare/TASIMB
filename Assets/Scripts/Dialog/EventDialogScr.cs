using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventDialogScr : MonoBehaviour
{

    public DialogScr currentDialog;
    public PlayerConversations eventConvo;
    public float timeDelay = 2f;


    public void triggerDialog()
    {
        eventConvo.InitiateDialog(currentDialog);
        Invoke("endDialog",timeDelay);
    }

    public void endDialog()
    {
        eventConvo.closeDialog();
    }
}
