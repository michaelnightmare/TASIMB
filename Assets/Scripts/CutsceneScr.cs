using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CutsceneScr : MonoBehaviour
{ 
    public AIEnemy[] enemyAis;
    public Transform[] enemyPosition;
    bool cutscenePlayed = false;
    private GameObject player;

    public DialogScr cutsceneDialog;
    public PlayerConversations playerConvo;
    public SimpleCameraTarget camScript;
    public Transform camCutsceneTarget;
    public UnityEvent OnCutsceneFinish = new UnityEvent();

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
        if (cutscenePlayed) return; 

        if (other.gameObject.layer == 9)
        {
            player = other.gameObject;
            cutscenePlayed = true;
            StartCoroutine(RunningCutscene());
       
        }

    }
    IEnumerator RunningCutscene()
    {
        player.SendMessage("disablePlayerMovement");

        camScript.SetFollowTarget(camCutsceneTarget);

        for(int i=0; i < enemyAis.Length; i++)
        {
            enemyAis[i].gameObject.SetActive(true);
            enemyAis[i].DisableAi();
            enemyAis[i].enemyMoveToPoint(enemyPosition[i].position);
        }

        playerConvo.InitiateDialog(cutsceneDialog);

        while(playerConvo.currentDialog != null)
        {
            yield return null;
        }

        for (int i = 0; i < enemyAis.Length; i++)
        {
            enemyAis[i].EnableAi();
        }

        camScript.Reset();

        player.SendMessage("enablePlayerMovement");

        OnCutsceneFinish.Invoke();

        yield break;
    }


}
