using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class MainMenuScript : MonoBehaviour {

    public Animator animator;
    public PlayableDirector sequence;

    public void Start()
    {
        sequence.Play();
        Debug.Log("Loaded Main Menu");
        Invoke("FadeInToScene", 5);
    }


    void FadeInToScene()
    {
        animator.SetTrigger("FadeIn");
    }

    void ChangeLevel()
    {
       SceneManager.LoadScene("NewGameScene");
    }

    public void StartGame()
    {
        animator.SetTrigger("FadeOut");
        Invoke("ChangeLevel", 1);
    }


}
