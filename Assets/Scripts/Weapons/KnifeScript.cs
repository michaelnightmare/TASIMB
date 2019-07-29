using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class KnifeScript : MonoBehaviour
{
    public GameObject knife;
    public Collider hitCollider;
    public bool KnifeActive = false; 
  

    Animator anims;
    // Use this for initialization
    void Start ()
    {
        anims = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        //KnifeActive = CrossPlatformInputManager.GetButtonDown("KnifeActive");

        if (Input.GetKey(KeyCode.CapsLock) || CrossPlatformInputManager.GetButton("KnifeActive"))
          
        {
           
            knife.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Mouse0)||CrossPlatformInputManager.GetButton("Slash")|| Input.GetAxis("Shoot") > 0f)
            {
                anims.SetTrigger("Slash");
            }
        }

        else
        {
            knife.gameObject.SetActive(false);

        }

    
    }

    public void EnableHitCollider()
    {
        hitCollider.enabled = true;
    }
    public void DisableHitCollider()
    {
        hitCollider.enabled = false;
    }

}
