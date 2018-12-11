using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour {

    public Animator animator;

    public void Start()
    {
        Invoke("FadeInToScene", 5);
    }


    void FadeInToScene()
    {
        animator.SetTrigger("FadeIn");
    }

    void ChangeLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void StartGame()
    {
        animator.SetTrigger("FadeOut");
        Invoke("ChangeLevel", 1);
    }


}
