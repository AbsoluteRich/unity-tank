using UnityEngine;
using System.IO;

public class HighScores : MonoBehaviour
{
    public string scoreFile = "scores.txt";  // The filename where high scores will be stored
    string currentDirectory;  // The path to the directory containing the high score file
    int[] scores = new int[10];  // Stores the loaded high scores

    private void Awake()
    {
        // dataPath is the project's assets folder
        // When built, dataPath becomes the Data folder inside the program directory
        currentDirectory = Application.dataPath;
    }

    public void LoadFile()
    {
        // Loads high scores
        bool fileExists = File.Exists(currentDirectory + "\\" + scoreFile);
        
        if (fileExists)
        {
            Debug.Log("File found!");
            
            // Recreates the high scores array to make sure it's empty
            scores = new int[scores.Length];
            
            // Reads each line of the file until the end or the score array is full
            StreamReader fileReader = new StreamReader(currentDirectory + "\\" + scoreFile);
            int scoreCount = 0;

            while (fileReader.Peek() != 0 && scoreCount < scores.Length)
            {
                string fileLine = fileReader.ReadLine();
                int readScore = -1;  // The score that has been read
                bool didParse = int.TryParse(fileLine, out readScore);

                if (didParse)
                {
                    // If the stored score was sucessfully converted, add it to the array...
                    scores[scoreCount] = readScore;
                }
                else
                {
                    // ...otherwise, use a default score
                    Debug.Log($"Invalid line in scores file at {scoreCount}, using default value.");
                    scores[scoreCount] = 0;
                }

                scoreCount++;
            }
            
            // Closes the file
            fileReader.Close();
        }
        else
        {
            Debug.Log($"'{scoreFile}' not found! No scores loaded");
        }
    }

    public void SaveFile()
    {
        // Opens the file for writing
        StreamWriter fileWriter = new StreamWriter(currentDirectory + "\\" + scoreFile);
        
        // Iterates through the scores array, writing each high score
        for (int i = 0; i < scores.Length; i++)
        {
            fileWriter.WriteLine(scores[i]);
        }
        
        // Closes the file
        fileWriter.Close();
        Debug.Log("Wrote to file");
    }

    public void SetScore(int[] newScores)
    {
        scores = newScores;
        SaveFile();
    }

    public int[] GetScores()
    {
        LoadFile();
        return scores;
    }
}
