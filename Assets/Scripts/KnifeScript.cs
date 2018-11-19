using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeScript : MonoBehaviour
{
    public GameObject knife;

    Animator anims;
    // Use this for initialization
    void Start ()
    {
        anims = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
           
            knife.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                
            
                anims.SetTrigger("Slash");
            }
        }

        else
        {

            knife.gameObject.SetActive(false);

        }
    }
}
