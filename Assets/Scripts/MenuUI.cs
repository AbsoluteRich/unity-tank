using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    public GameObject exitMenu;
    
    public void LoadScene(string sceneName)
    {
        Debug.Log("Playing game!");
        SceneManager.LoadScene(sceneName);
    }

    public void Exit()
    {
        Debug.Log("Exited out of game!");
        Application.Quit();
    }

    public void ExitMenu(bool exiting)
    {
        exitMenu.SetActive(exiting);
    }
}
