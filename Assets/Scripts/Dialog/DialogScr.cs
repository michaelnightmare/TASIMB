using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogScr : MonoBehaviour
{
    public List<string> dialog = new List<string>();
 
    public GameObject OverHeadUIPrefab;
    GameObject spawnedOverheadRef;
    AudioSource NPCSounds;
    public AudioClip howdyClip;
    public bool disableDialogOnceComplete;
    public UnityEvent dialogFinished = new UnityEvent();
    public UnityEvent dialogStarted = new UnityEvent();

    QuestSystem quests;

    void Start()
    {
        NPCSounds = GetComponent<AudioSource>();
        quests = QuestSystem.quests;
        dialogFinished.AddListener(delegate { quests.NotifyDialog(gameObject); });
    }


    void Update()
    {
        if(spawnedOverheadRef !=null && spawnedOverheadRef.activeSelf)
        {
            spawnedOverheadRef.transform.position = transform.position;
        }

    }

   public void showOverhead()
    {
        if (spawnedOverheadRef != null)
        {
            spawnedOverheadRef.SetActive(true);
        }
        else
        {
            spawnedOverheadRef = Instantiate(OverHeadUIPrefab, transform.position, Quaternion.identity);
        }

    }
    public void hideOverhead()
    {
        if(spawnedOverheadRef != null)
        {
            spawnedOverheadRef.SetActive(false);
         
        }
        else
        {
            
        }

    }
}


