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
    
    void Awake()
    {
        m_GameState = GameStart.Start;
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
            m_GameState = m_GameState.Playing;
            SetTanksEnable(true);
        }
    }

    void GS_Playing()
    {
        Debug.Log("We are in the playing game state!");
    }

    void GS_GameOver()
    {
        Debug.Log("We are in the game over game state!");
    }
}
