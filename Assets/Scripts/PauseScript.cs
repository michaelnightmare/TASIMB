using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour {

    public string mainMenuSceneName = "NewMainMenu";

    public bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public Button MainMenuButton;
    public Button QuitGameButton;
    public Button CreditsButton;
    public GameObject creditsScreen;
    public bool creditsOnScreen = false; 
    
   
	
	
	void Update ()
    {
		if(Input.GetButtonDown("StartButton"))
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
        if(Input.GetButtonDown("A")&& GameIsPaused==true && creditsOnScreen==false)
        {
           MainMenuButton.onClick.Invoke();
        }
        if(Input.GetButtonDown("B")&& GameIsPaused==true && creditsOnScreen==false)
        {
            QuitGameButton.onClick.Invoke();
        }
        if (Input.GetButtonDown("Y") && GameIsPaused == true && creditsOnScreen == false)
        {
            showCredits();
          
        }
        if (Input.GetButtonDown("X") && GameIsPaused == true && creditsOnScreen == true)
        {
            hideCredits();

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
    }
 
    public void LoadMainMenu()
    {
     
        SceneManager.LoadScene("NewMainMenu");
    }

    public void showCredits()
    {
        creditsOnScreen = true;
        creditsScreen.SetActive(true);
   

    }
    public void hideCredits()
    {
        creditsOnScreen = false;
        creditsScreen.SetActive(false);
      
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
