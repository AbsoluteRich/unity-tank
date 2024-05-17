using System;
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
    
    private void Awake()
    {
        m_GameState = GameState.Start;
        m_MessageTxt.text = "Press Enter to start...";
        m_isPaused = false;
        m_AudioSource = GetComponent<AudioSource>();
        m_AudioSource.clip = m_PlayingMusic;
        m_AudioSource.Play();
    }
    
    void Start()
    {
        SetTanksEnable(false);
        bestTimes = m_HighScores.GetScores();
        int minutes = Mathf.FloorToInt(bestTimes[0] / 60f);
        int seconds = Mathf.FloorToInt(bestTimes[0] % 60);
        m_BestTimeTxt.text = string.Format("{0:0}:{1:00}", minutes, seconds);
    }

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

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

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
    
    void SetTanksEnable(bool enabled)
    {
        for (int i = 0; i < m_Tanks.Length; i++)
        {
            m_Tanks[i].SetActive(enabled);
        }
    }

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

    void GS_Playing()
    {
        Debug.Log("We are in the playing game state!");
        bool gameOver = false;
        m_GameTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(m_GameTime / 60f);
        int seconds = Mathf.FloorToInt(m_GameTime % 60);
        m_TimeTxt.text = string.Format("{0:0}:{1:00}", minutes, seconds);
        
        if (OneTankLeft() == true)
        {
            gameOver = true;
            m_MessageTxt.text = "You win!";
            SetTimes(Mathf.FloorToInt(m_GameTime));
            m_HighScores.SetScore(bestTimes);
            minutes = Mathf.FloorToInt(m_GameTime / 60f);
            seconds = Mathf.FloorToInt(m_GameTime % 60);
            m_BestTimeTxt.text = string.Format("{0:0}:{1:00}", minutes, seconds);
        }
        else if (IsPlayerDead() == true)
        {
            gameOver = true;
            m_MessageTxt.text = "You lose!";
        }
        if (gameOver == true)
        {
            m_GameState = GameState.GameOver;
        }
    }

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

    bool IsPlayerDead()
    {
        for (int i = 0; i < m_Tanks.Length; i++)
        {
            if (m_Tanks[i].tag == "Player")
            {
                return false;
            }
        }
        return true;
    }

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
