using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogScr : MonoBehaviour
{
    public List<string> dialog = new List<string>();
    public GameObject OverHeadUIPrefab;
    GameObject spawnedOverheadRef;


    void Start()
    {

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


