using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    public GameObject exitMenu;
    public Text m_HighScoreMsg;
    public HighScores m_HighScores;
    int[] m_Scores;
    
    /// <summary>
    /// Initalises the main menu by setting up the high score panel.
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
        m_Scores = m_HighScores.GetScores();
        string text = "";
        for (int i = 0; i < m_Scores.Length; i++)
        {
            int seconds = m_Scores[i];
            text += string.Format("{0:D2}:{1:D2}\n", (seconds / 60), (seconds % 60));
        }
        m_HighScoreMsg.text = text;
    }
}
