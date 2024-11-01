using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    public GameObject exitMenu;
    public Text mHighScoreMsg;
    public HighScores mHighScores;
    int[] _mScores;
    
    /// <summary>
    /// Initialises the main menu by setting up the high score panel.
    /// </summary>
    private void Start()
    {
        DisplayHighScores();
    }
    
    /// <summary>
    /// Loads the given scene.
    /// </summary>
    /// <param name="sceneName">The name of the scene to load.</param>
    public void LoadScene(string sceneName)
    {
        Debug.Log("Playing game!");
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1f;
    }
    
    /// <summary>
    /// Closes the game.
    /// </summary>
    public void Exit()
    {
        Debug.Log("Exited out of game!");
        Application.Quit();
    }
    
    /// <summary>
    /// Controls the visibility of the exit menu.
    /// </summary>
    /// <param name="exiting">If set to <c>true</c>, displays the exit menu. Otherwise, hides it.</param>
    public void ExitMenu(bool exiting)
    {
        exitMenu.SetActive(exiting);
    }

    /// <summary>
    /// Sets the high scores section in the main menu to the scores in the high scores file.
    /// </summary>
    private void DisplayHighScores()
    {
        _mScores = mHighScores.GetScores();
        string text = "";
        foreach (var seconds in _mScores)
        {
            text += $"{(seconds / 60):D2}:{(seconds % 60):D2}\n";
        }
        mHighScoreMsg.text = text;
    }
}
