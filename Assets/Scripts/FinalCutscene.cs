using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FinalCutscene : MonoBehaviour
{ 
    public List<AIEnemy> enemyAis;
    public Transform[] enemyPosition;
    bool cutscenePlayed = false;
    private GameObject player;

    public DialogScr cutsceneDialog;
    public PlayerConversations playerConvo;
    public SimpleCameraTarget camScript;
    public Transform camCutsceneTarget;
    public Transform camCutsceneTarget2;
    public GameObject Train;
    Animator trainAnim;

    //public UnityEvent OnCutsceneFinish = new UnityEvent();

	// Use this for initialization
	void Start ()
    {
        trainAnim = Train.GetComponent<Animator>();
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

        Train.SetActive(true);

        bool trainAnimFinished = false;
        while (!trainAnimFinished)
        {
            AnimatorClipInfo[] infos = trainAnim.GetCurrentAnimatorClipInfo(0);

            if(infos.Length == 0)
            {
                trainAnimFinished = true;
            }

            yield return null;
        }

        Debug.Log("finished");

        camScript.SetFollowTarget(camCutsceneTarget2);

        for (int i=0; i < enemyAis.Count; i++)
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

        for (int i = 0; i < enemyAis.Count; i++)
        {
            enemyAis[i].EnableAi();
            AIEnemy ai = enemyAis[i];
            ai.OnEnemyDeath.AddListener(delegate { EnemyDied(ai); });
        }

        camScript.Reset();

        player.SendMessage("enablePlayerMovement");

        //OnCutsceneFinish.Invoke();

        yield break;
    }


    public void EnemyDied(AIEnemy enemy)
    {
        if (enemyAis.Contains(enemy)) enemyAis.Remove(enemy);
        CheckAllDead();
    }

    public void WolfDied(AIWolf wolf)
    {

    }

    void CheckAllDead()
    {
        if (enemyAis.Count == 0 && enemyAis.Count == 0)
        {
            QuestSystem.quests.NotifyKill(gameObject);
        }
    }

}
