using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class fogTrigger : MonoBehaviour
{
    public UnityEvent onEnterTriggerZone = new UnityEvent();

    bool unlocked = false;
    
    public Material mat;

    void Start()
    {
        mat = gameObject.GetComponent<MeshRenderer>().material;
    }

    void OnTriggerEnter(Collider other)
    {
        if (unlocked) return;

        if (other.gameObject.layer == 9)
        {
            onEnterTriggerZone.Invoke();
           
            unlocked = true;

             while (mat.color.a > 0)
        {
            Color newColor = mat.color;
            newColor.a -= Time.deltaTime;
            mat.color = newColor;
            gameObject.GetComponent<MeshRenderer>().material = mat;
        }

        }

    }
    





}
