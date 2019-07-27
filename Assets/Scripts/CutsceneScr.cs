using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CutsceneScr : MonoBehaviour
{
    public bool enabled = true;
    public List<AIEnemy> enemyAis = new List<AIEnemy>();
    public List<AIWolf> alienAis = new List<AIWolf>();
    public Transform[] enemyPosition;
    public Transform[] alienPosition; 
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

    public void EnableCutscene()
    {
        enabled = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (cutscenePlayed || !enabled) return; 

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

        for(int i=0; i < enemyAis.Count; i++ )
        {
            enemyAis[i].gameObject.SetActive(true);
            enemyAis[i].DisableAi();
            enemyAis[i].enemyMoveToPoint(enemyPosition[i].position);

        }

        for (int i = 0; i < alienAis.Count; i++)
        {
  

            alienAis[i].gameObject.SetActive(true);
            alienAis[i].DisableAi();
            alienAis[i].enemyMoveToPoint(alienPosition[i].position);
        }



        playerConvo.InitiateDialog(cutsceneDialog);

        while(playerConvo.currentDialog != null)
        {
            yield return null;
        }

        for (int i = 0; i < enemyAis.Count; i++)
        {
            AIEnemy ai = enemyAis[i];
            enemyAis[i].EnableAi();
            enemyAis[i].OnEnemyDeath.AddListener(delegate { EnemyDied(ai); });
        }

        for (int i = 0; i < alienAis.Count; i++)
        {
            AIWolf wolf = alienAis[i];
            alienAis[i].EnableAi();
            alienAis[i].OnEnemyDeath.AddListener(delegate { WolfDied(wolf); });
        }

      

        camScript.Reset();

        player.SendMessage("enablePlayerMovement");

        OnCutsceneFinish.Invoke();

        yield break;
    }

    public void EnemyDied(AIEnemy enemy)
    {
        if(enemyAis.Contains(enemy)) enemyAis.Remove(enemy);
        CheckAllDead();
    }

    public void WolfDied(AIWolf wolf)
    {
        if (alienAis.Contains(wolf)) alienAis.Remove(wolf);
        CheckAllDead();
    }

    void CheckAllDead()
    {
        if(alienAis.Count == 0 && enemyAis.Count == 0)
        {
            QuestSystem.quests.NotifyKill(gameObject);
        }
    }


}
