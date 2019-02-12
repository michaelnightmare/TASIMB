using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunDisplayScr : MonoBehaviour
{
    public CanvasGroup gunDisplay;
    public bool displaying = false;
    float holdTime = 2f;
    float fadeOutTime = .2f;
    float fadeAnimTime = 0f; 


    public void displayOn()
    {
        displaying = true;
        fadeAnimTime = holdTime + fadeOutTime;

    }

    void updateGunDisplay()
    {
        float alpha = Mathf.Clamp(fadeAnimTime / fadeOutTime, 0f, 1f);
        gunDisplay.GetComponent<CanvasGroup>().alpha = alpha;
    }


    void Start ()
    {
		
	}


    void Update()
    {
        if(fadeAnimTime > 0)
        {
            fadeAnimTime -= Time.deltaTime;
            if(fadeAnimTime < 0)
            {
                fadeAnimTime = 0;
            }

            updateGunDisplay();
        }
    }
}
