using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    [Header("Configuración")]

    public string settingsScene;
    public GameObject panelPause;
    public static bool isPaused = false;   

    void Start()
    {
        Time.timeScale = 1;
    }
       
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                isPaused = true;
                panelPause.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                ContinueButton();
            }
        }
        */
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("Main");
    }

    public void QuitButton()
    {
        Debug.Log("Estas fuera del juego..");
        Application.Quit();
        
    }

    public void ContinueButton()
    {

        
            if (isPaused) 
            {
                isPaused = false;
                panelPause.SetActive(false);
                Time.timeScale = 1;
            }
                                    
            
        
    }
    public void SettingsScene()
    {
        if (!string.IsNullOrEmpty(settingsScene))
        {
            SceneManager.LoadScene(settingsScene);
        }
        else
        {
            Debug.Log("El nombre de la escena o la escena, no esta configurado correctamente");
        }
    }
    public void MenuScene()
    {
        SceneManager.LoadScene("Menu");
    }

}
