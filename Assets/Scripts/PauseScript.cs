using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseScript : MonoBehaviour {

    public string mainMenuSceneName = "NewMainMenu";

    public EventSystem es;

    public bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public Button MainMenuButton;
    public Button QuitGameButton;
    public Button CreditsButton;
    public Button TutorialButton;
    public Button ResumeButton;
    public Button ExitButton;

    public GameObject creditsScreen;
    public GameObject tutorialScreen;
    public bool tutorialOnScreen = false; 
    public bool creditsOnScreen = false; 
    
   
	
	
	void Update ()
    {
		if(Input.GetButtonDown("StartButton") || Input.GetKeyDown(KeyCode.Escape) && creditsOnScreen==false && tutorialOnScreen==false)
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                PauseGame();
                    
            }

        }
        //if(Input.GetButtonDown("A")&& GameIsPaused==true && creditsOnScreen==false)
        //{
        //   MainMenuButton.onClick.Invoke();
        //}
        //if(Input.GetButtonDown("B")&& GameIsPaused==true && creditsOnScreen==false)
        //{
        //    QuitGameButton.onClick.Invoke();
        //}
        //if (Input.GetButtonDown("Y") && GameIsPaused == true && creditsOnScreen == false)
        //{
        //    showCredits();

        //}
        if (Input.GetButtonDown("B") && GameIsPaused == true && creditsOnScreen == true)
        {
            hideCredits();

        }
        ////if (Input.GetButtonDown("X") && GameIsPaused == true && tutorialOnScreen == false)
        ////{
        ////    showTutorial();

        //}
        if (Input.GetButtonDown("B") && GameIsPaused == true && tutorialOnScreen == true)
        {
            hideTutorial();

        }




    }


    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        es.SetSelectedGameObject(ResumeButton.gameObject);
    }
 
    public void LoadMainMenu()
    {
     
        SceneManager.LoadScene("NewMainMenu");
    }

    public void showCredits()
    {
        creditsOnScreen = true;
        creditsScreen.SetActive(true);
        pauseMenuUI.SetActive(false);
        GameIsPaused = true; 
   

    }
    public void hideCredits()
    {
        creditsOnScreen = false;
        creditsScreen.SetActive(false);
        pauseMenuUI.SetActive(true);
      
    }
    public void showTutorial()
    {
        tutorialOnScreen = true;
        tutorialScreen.SetActive(true);
        pauseMenuUI.SetActive(false);
        GameIsPaused = true;


    }
    public void hideTutorial()
    {
        tutorialOnScreen = false;
        tutorialScreen.SetActive(false);
        pauseMenuUI.SetActive(true);

    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }

    public void OnDisable()
    {
        Time.timeScale = 1f;
    }

}
