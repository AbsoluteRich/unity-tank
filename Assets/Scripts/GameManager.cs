using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    
    private void Awake()
    {
        m_GameState = GameState.Start;
    }

    void SetTanksEnable(bool enabled)
    {
        for (int i = 0; i < m_Tanks.Length; i++)
        {
            m_Tanks[i].SetActive(enabled);
        }
    }
    
    void Start()
    {
        SetTanksEnable(false);
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
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

    void GS_Start()
    {
        Debug.Log("We are in the starting game state!");
        if (Input.GetKeyUp(KeyCode.Return) == true)
        {
            m_GameState = GameState.Playing;
            SetTanksEnable(true);
        }
    }

    void GS_Playing()
    {
        Debug.Log("We are in the playing game state!");
        bool gameOver = false;
        m_GameTime += Time.deltaTime;

        if (IsPlayerDead() == true)
        {
            gameOver = true;
            Debug.Log("You lose!");
        }
        else if (OneTankLeft() == true)
        {
            gameOver = true;
            Debug.Log("You win!");
            SetTimes(Mathf.FloorToInt(m_GameTime));
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
        return tanksRemaining <= 1;
    }

    bool IsPlayerDead()
    {
        for (int i = 0; i < m_Tanks.Length; i++)
        {
            if (m_Tanks[i].tag == "Player")
            {
                return true;
            }
        }
        return false;
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
