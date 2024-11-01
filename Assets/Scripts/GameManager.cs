using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] mTanks;
    private float _mGameTime;

    private enum GameState
    {
        Start,
        Playing,
        GameOver
    };
    private GameState _mGameState;
    public int[] bestTimes = new int[10];
    public HighScores mHighScores;
    public Text mTimeTxt;
    public Text mEnemyTanksTxt;
    public Text mBestTimeTxt;
    public Text mMessageTxt;
    public GameObject mPauseMenu;
    public static bool MIsPaused;
    AudioSource _mAudioSource;
    public AudioClip mPlayingMusic;
    
    /// <summary>
    /// Sets the initial game state and starts music.
    /// </summary>
    private void Awake()
    {
        _mGameState = GameState.Start;
        mMessageTxt.text = "Press Enter to start...";
        MIsPaused = false;
        _mAudioSource = GetComponent<AudioSource>();
        _mAudioSource.clip = mPlayingMusic;
        _mAudioSource.Play();
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
        MIsPaused = pausing;
        mPauseMenu.SetActive(pausing);
        Time.timeScale = pausing ? 0f : 1f;
    }
    
    /// <summary>
    /// Disables all tanks and displays the high score.
    /// </summary>
    void Start()
    {
        SetTanksEnable(false);
        bestTimes = mHighScores.GetScores();
        int minutes = Mathf.FloorToInt(bestTimes[0] / 60f);
        int seconds = Mathf.FloorToInt(bestTimes[0] % 60);
        mBestTimeTxt.text = $"{minutes:0}:{seconds:00}";
    }
    
    /// <summary>
    /// Pauses the game when the Esc key is pressed and handles game state.
    /// </summary>
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            PauseGame(!MIsPaused);
        }
        
        switch (_mGameState)
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
    void SetTanksEnable(bool isEnabled)
    {
        foreach (var t in mTanks)
        {
            t.SetActive(isEnabled);
        }
    }
    
    /// <summary>
    /// Handler for the starting game state. Starts the game when the Enter key is pressed, setting all text and enabling tanks.
    /// </summary>
    void GS_Start()
    {
        Debug.Log("We are in the starting game state!");
        
        if (Input.GetKeyUp(KeyCode.Return))
        {
            _mGameState = GameState.Playing;
            mEnemyTanksTxt.text = (mTanks.Length - 1).ToString();  // -1 removes the player tank from the count
            SetTanksEnable(true);
            mMessageTxt.text = null;
        }
    }
    
    /// <summary>
    /// Handler for the playing game state. The game ends when the win condition (player is the last tank standing) or lose condition (player dies) is met.
    /// </summary>
    void GS_Playing()
    {
        Debug.Log("We are in the playing game state!");
        bool gameOver = false;
        _mGameTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(_mGameTime / 60f);
        int seconds = Mathf.FloorToInt(_mGameTime % 60);
        mTimeTxt.text = $"{minutes:0}:{seconds:00}";


        if (IsPlayerDead())
        {
            gameOver = true;
            mMessageTxt.text = "You lose!";
        }
        else if (OneTankLeft())
        {
            gameOver = true;
            mMessageTxt.text = "You win!";
            SetTimes(Mathf.FloorToInt(_mGameTime));
            mHighScores.SetScore(bestTimes);
            minutes = Mathf.FloorToInt(_mGameTime / 60f);
            seconds = Mathf.FloorToInt(_mGameTime % 60);
            mBestTimeTxt.text = $"{minutes:0}:{seconds:00}";
        }
        
        if (gameOver)
        {
            _mGameState = GameState.GameOver;
        }
    }
    
    /// <summary>
    /// Handler for the game over state. Restarts the game when the Enter key is pressed.
    /// </summary>
    void GS_GameOver()
    {               
        Debug.Log("We are in the game over game state!");
        if (Input.GetKeyUp(KeyCode.Return))
        {
            _mGameTime = 0;
            _mGameState = GameState.Playing;
            
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
        foreach (var t in mTanks)
        {
            if (t.activeSelf)
            {
                tanksRemaining++;
            }
        }

        mEnemyTanksTxt.text = (tanksRemaining - 1).ToString();
        return tanksRemaining <= 1;
    }
    
    /// <summary>
    /// Checks if the player is dead.
    /// </summary>
    /// <returns><c>true</c> if the player is dead, otherwise <c>false</c>.</returns>
    bool IsPlayerDead()
    {
        foreach (var t in mTanks)
        {
            if (t.activeSelf == false)
            {
                if (t.CompareTag("Player"))
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

        for (int i = 0; i < bestTimes.Length; i++)
        {
            if (bestTimes[i] > newTime || bestTimes[i] == 0)
            {
                (bestTimes[i], newTime) = (newTime, bestTimes[i]);
            }
        }
        Debug.Log($"Time to beat = {bestTimes[0]}");
    }
}
