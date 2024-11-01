using UnityEngine;
using System.IO;

public class HighScores : MonoBehaviour
{
    public string scoreFile = "scores.txt";  // The filename where high scores will be stored
    string _currentDirectory;  // The path to the directory containing the high score file
    int[] _scores = new int[10];  // Stores the loaded high scores
    
    /// <summary>
    /// Initialises the high score file directory to dataPath.
    /// (dataPath is the project's assets folder. When built, dataPath becomes the Data folder inside the program directory.)
    /// </summary>
    private void Awake()
    {
        _currentDirectory = Application.dataPath;
    }
    
    /// <summary>
    /// Loads scores from the high score file, adding them to the high score array.
    /// </summary>
    private void LoadFile()
    {
        bool fileExists = File.Exists(Path.Combine(_currentDirectory, scoreFile));
        
        if (fileExists)
        {
            Debug.Log("File found!");
            
            // Recreates the high scores array to make sure it's empty
            _scores = new int[_scores.Length];
            
            // Reads each line of the file until the end or the score array is full
            StreamReader fileReader = new StreamReader(Path.Combine(_currentDirectory, scoreFile));
            int scoreCount = 0;

            while (fileReader.Peek() != 0 && scoreCount < _scores.Length)
            {
                string fileLine = fileReader.ReadLine();
                bool didParse = int.TryParse(fileLine, out var readScore);

                if (didParse)
                {
                    // If the stored score was successfully converted, add it to the array...
                    _scores[scoreCount] = readScore;
                }
                else
                {
                    // ...otherwise, use a default score
                    Debug.Log($"Invalid line in scores file at {scoreCount}, using default value.");
                    _scores[scoreCount] = 0;
                }

                scoreCount++;
            }

            fileReader.Close();
        }
        else
        {
            Debug.Log($"'{scoreFile}' not found! No scores loaded");
        }
    }
    
    /// <summary>
    /// Writes the contents of the high score array to the high score file.
    /// </summary>
    private void SaveFile()
    {
        StreamWriter fileWriter = new StreamWriter(Path.Combine(_currentDirectory, scoreFile));

        foreach (var t in _scores)
        {
            fileWriter.WriteLine(t);
        }

        fileWriter.Close();
        Debug.Log("Wrote to file");
    }
    
    /// <summary>
    /// Sets the high score array to the given scores and writes it to the high score file.
    /// </summary>
    /// <param name="newScores">An array of scores.</param>
    public void SetScore(int[] newScores)
    {
        _scores = newScores;
        SaveFile();
    }
    
    /// <summary>
    /// Reads the high scores from the save file.
    /// </summary>
    /// <returns>An array of scores.</returns>
    public int[] GetScores()
    {
        LoadFile();
        return _scores;
    }
}
