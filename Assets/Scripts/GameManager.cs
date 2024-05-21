using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] m_Tanks;
    private float m_GameTime = 0;
    public enum GameState
    {
        Start,
        Playing,
        GameOver
    };
    private GameState m_GameState;
    public int[] bestTimes = new int[10];
    public HighScores m_HighScores;
    public Text m_TimeTxt;
    public Text m_EnemyTanksTxt;
    public Text m_BestTimeTxt;
    public Text m_MessageTxt;
    public GameObject m_PauseMenu;
    public static bool m_isPaused;
    AudioSource m_AudioSource;
    public AudioClip m_PlayingMusic;
    
    /// <summary>
    /// Sets the initial game state and starts music.
    /// </summary>
    private void Awake()
    {
        m_GameState = GameState.Start;
        m_MessageTxt.text = "Press Enter to start...";
        m_isPaused = false;
        m_AudioSource = GetComponent<AudioSource>();
        m_AudioSource.clip = m_PlayingMusic;
        m_AudioSource.Play();
    }
    
    /// <summary>
    /// Loads the main menu.
    /// </summary>
    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// Pauses the game, preventing most actions and displaying the pause menu.
    /// </summary>
    public void PauseGame(bool pausing)
    {
        m_isPaused = pausing;
        m_PauseMenu.SetActive(pausing);
        if (pausing == true)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
    
    /// <summary>
    /// Disables all tanks and displays the high score.
    /// </summary>
    void Start()
    {
        SetTanksEnable(false);
        bestTimes = m_HighScores.GetScores();
        int minutes = Mathf.FloorToInt(bestTimes[0] / 60f);
        int seconds = Mathf.FloorToInt(bestTimes[0] % 60);
        m_BestTimeTxt.text = string.Format("{0:0}:{1:00}", minutes, seconds);
    }
    
    /// <summary>
    /// Pauses the game when the Esc key is pressed and handles game state.
    /// </summary>
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            PauseGame(!m_isPaused);
        }
        
        switch (m_GameState)
        {
            case GameState.Start:
                GS_Start();
                break;
            case GameState.Playing:
                GS_Playing();
                break;
            case GameState.GameOver:
                GS_GameOver();
                break;
        }
    }
    
    /// <summary>
    /// Enables/disables all tanks.
    /// </summary>
    void SetTanksEnable(bool enabled)
    {
        for (int i = 0; i < m_Tanks.Length; i++)
        {
            m_Tanks[i].SetActive(enabled);
        }
    }
    
    /// <summary>
    /// Handler for the starting game state. Starts the game when the Enter key is pressed, setting all text and enabling tanks.
    /// </summary>
    void GS_Start()
    {
        Debug.Log("We are in the starting game state!");
        
        if (Input.GetKeyUp(KeyCode.Return) == true)
        {
            m_GameState = GameState.Playing;
            m_EnemyTanksTxt.text = (m_Tanks.Length - 1).ToString();  // -1 removes the player tank from the count
            SetTanksEnable(true);
            m_MessageTxt.text = null;
        }
    }
    
    /// <summary>
    /// Handler for the playing game state. The game ends when the win condition (player is the last tank standing) or lose condition (player dies) is met.
    /// </summary>
    void GS_Playing()
    {
        Debug.Log("We are in the playing game state!");
        bool gameOver = false;
        m_GameTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(m_GameTime / 60f);
        int seconds = Mathf.FloorToInt(m_GameTime % 60);
        m_TimeTxt.text = string.Format("{0:0}:{1:00}", minutes, seconds);


        if (IsPlayerDead() == true)
        {
            gameOver = true;
            m_MessageTxt.text = "You lose!";
        }
        else if (OneTankLeft() == true)
        {
            gameOver = true;
            m_MessageTxt.text = "You win!";
            SetTimes(Mathf.FloorToInt(m_GameTime));
            m_HighScores.SetScore(bestTimes);
            minutes = Mathf.FloorToInt(m_GameTime / 60f);
            seconds = Mathf.FloorToInt(m_GameTime % 60);
            m_BestTimeTxt.text = string.Format("{0:0}:{1:00}", minutes, seconds);
        }
        
        if (gameOver == true)
        {
            m_GameState = GameState.GameOver;
        }
    }
    
    /// <summary>
    /// Handler for the game over state. Restarts the game when the Enter key is pressed.
    /// </summary>
    void GS_GameOver()
    {               
        Debug.Log("We are in the game over game state!");
        if (Input.GetKeyUp(KeyCode.Return) == true)
        {
            m_GameTime = 0;
            m_GameState = GameState.Playing;
            
            SetTanksEnable(false);
            SetTanksEnable(true);

        }
    }
    
    /// <summary>
    /// Checks if the player tank is the only one that remains.
    /// </summary>
    /// <returns><c>true</c> if the player is the last tank, otherwise <c>false</c>.</returns>
    bool OneTankLeft()
    {
        int tanksRemaining = 0;
        for (int i = 0; i < m_Tanks.Length; i++)
        {
            if (m_Tanks[i].activeSelf == true)
            {
                tanksRemaining++;
            }
        }

        m_EnemyTanksTxt.text = (tanksRemaining - 1).ToString();
        return tanksRemaining <= 1;
    }
    
    /// <summary>
    /// Checks if the player is dead.
    /// </summary>
    /// <returns><c>true</c> if the player is dead, otherwise <c>false</c>.</returns>
    bool IsPlayerDead()
    {
        for (int i = 0; i < m_Tanks.Length; i++)
        {
            if (m_Tanks[i].activeSelf == false)
            {
                if (m_Tanks[i].tag == "Player")
                    return true;
            }
        }
        return false;
    }
    
    /// <summary>
    /// Adds the given time to the top 10 scores if it's higher than the rest.
    /// </summary>
    /// <param name="newTime">The game time to check.</param>
    void SetTimes(int newTime)
    {
        if (newTime <= 0)
        {
            return;
        }

        int tempTime;
        for (int i = 0; i < bestTimes.Length; i++)
        {
            if (bestTimes[i] > newTime || bestTimes[i] == 0)
            {
                tempTime = bestTimes[i];
                bestTimes[i] = newTime;
                newTime = tempTime;
            }
        }
        Debug.Log($"Time to beat = {bestTimes[0]}");
    }
}
