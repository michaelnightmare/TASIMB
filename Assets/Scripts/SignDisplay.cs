using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignDisplay : MonoBehaviour
{
    public Image locSign;
    public float holdTime;
    public float fadeTime;

    bool initialized = false;
    Color startColor;
    Color clearColor;
    
    void Initialize()
    {

        locSign = GetComponent<Image>();
        startColor = locSign.color;
        clearColor = startColor;
        clearColor.a = 0;
        initialized = true;
    }


    IEnumerator ShowingSign()
    {
        //don't do anything for hold time seconds
        yield return new WaitForSeconds(holdTime);

        float t = 0;
        while(t < fadeTime)
        {
            t += Time.deltaTime;

            Color newColor = Color.Lerp(startColor, clearColor, t / fadeTime);
            locSign.color = newColor;

            yield return null;
        }


        gameObject.SetActive(false);
        yield break;
    }

    public void ShowSign()
    {
        gameObject.SetActive(true);

        //check for sign reference
        if (!initialized)
        {
            Initialize();
        }

        StartCoroutine(ShowingSign());
    }

}
