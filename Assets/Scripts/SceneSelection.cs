using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSelection : MonoBehaviour
{
    [Header("Configuración")]
    public string sceneToLoad;

    public void LoadScene()
    {
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.Log("El nombre de la escena o la escena, no esta configurado correctamente");
        }
    }




    public void QuitGame()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();

    }
}

