using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    public GameObject exitMenu;
    public Text m_HighScoreMsg;
    public HighScores m_HighScores;
    int[] m_Scores;
    AudioSource m_AudioSource;
    public AudioClip m_StartMusic;

    private void Start()
    {
        DisplayHighScores();
    }

    private void Awake()
    {
        m_AudioSource = GetComponent<AudioSource>();
        m_AudioSource.clip = m_StartMusic;
        m_AudioSource.Play();
    }
    
    public void LoadScene(string sceneName)
    {
        Debug.Log("Playing game!");
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1f;
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
