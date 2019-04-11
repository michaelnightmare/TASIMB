using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour {

    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public Button MainMenuButton;
    public Button QuitGameButton;
    
   
	
	
	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.P)||Input.GetButtonDown("StartButton"))
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
        if(Input.GetButtonDown("A")&& GameIsPaused==true)
        {
           MainMenuButton.onClick.Invoke();
        }
        if(Input.GetButtonDown("B")&& GameIsPaused==true)
        {
            QuitGameButton.onClick.Invoke();
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
    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }

}
