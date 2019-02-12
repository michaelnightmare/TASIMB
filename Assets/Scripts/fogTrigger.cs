using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class fogTrigger : MonoBehaviour
{
    public UnityEvent onEnterTriggerZone = new UnityEvent();

    bool unlocked = false;
    


    void Start()
    {
    
    }

    void OnTriggerEnter(Collider other)
    {
        if (unlocked) return;

        if (other.gameObject.layer == 9)
        {
            onEnterTriggerZone.Invoke();
           
            unlocked = true;


            gameObject.GetComponent<MeshRenderer>().enabled = false;
        }

    }
    





}
