using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestObjectiveHUD : MonoBehaviour
{
    public float holdTime = 1f;
    public float fadeTime = 1f;
    public float delay = 0.5f;

    //Signs
    public CanvasGroup acceptedSign;
    public CanvasGroup completedSign;

    //Text
    public Text headerText;
    public Text objectiveText;

    private void Start()
    {
        acceptedSign.gameObject.SetActive(false);
        completedSign.gameObject.SetActive(false);
    }

    public void ReceivedNewQuest(Objective o, bool isFirstQuest = false)
    {
        if (isFirstQuest)
        {
            SetObjective(o.questText);
            SetHeader(o.questName);
            return;
        }

        StartCoroutine(UpdatingHUDForNewQuest(o));
    }

    IEnumerator UpdatingHUDForNewQuest(Objective o, bool isFirstQuest = false)
    {
        float t = 1f;

        if (!isFirstQuest)
        {
            completedSign.gameObject.SetActive(true);
            completedSign.alpha = 1;
            yield return new WaitForSeconds(holdTime);
            t = 1;
            while (t > 0f)
            {
                t -= Time.deltaTime / fadeTime;
                completedSign.alpha = t;
                yield return null;
            }
            completedSign.gameObject.SetActive(false);
        }

        yield return new WaitForSeconds(delay);

        SetObjective(o.questText);
        SetHeader(o.questName);

        acceptedSign.gameObject.SetActive(true);
        acceptedSign.alpha = 1;
        yield return new WaitForSeconds(holdTime);

        t = 1;
        while (t > 0f)
        {
            t -= Time.deltaTime / fadeTime;
            acceptedSign.alpha = t;
            yield return null;
        }
        acceptedSign.gameObject.SetActive(false);

        yield break;
    }

    public void SetHeader(string t)
    {
        headerText.text = t;
    }

    public void SetObjective(string t)
    {
        objectiveText.text = t;
    }
}
