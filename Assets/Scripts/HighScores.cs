using UnityEngine;
using System.IO;

public class HighScores : MonoBehaviour
{
    public string scoreFile = "scores.txt";
    string currentDirectory;
    int[] scores = new int[10];

    private void Awake()
    {
        currentDirectory = Application.dataPath;
    }

    public void LoadFile()
    {
        bool fileExists = File.Exists(currentDirectory + "\\" + scoreFile);
        
        if (fileExists)
        {
            Debug.Log("File found!");
            scores = new int[scores.Length];
            StreamReader fileReader = new StreamReader(currentDirectory + "\\" + scoreFile);
            int scoreCount = 0;

            while (fileReader.Peek() != 0 && scoreCount < scores.Length)
            {
                string fileLine = fileReader.ReadLine();
                int readScore = -1;  // The score that has been read
                bool didParse = int.TryParse(fileLine, out readScore);

                if (didParse)
                {
                    scores[scoreCount] = readScore;
                }
                else
                {
                    Debug.Log($"Invalid line in scores file at {scoreCount}, using default value.");
                    scores[scoreCount] = 0;
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

    public void SaveFile()
    {
        StreamWriter fileWriter = new StreamWriter(currentDirectory + "\\" + scoreFile);
        for (int i = 0; i < scores.Length; i++)
        {
            fileWriter.WriteLine(scores[i]);
        }
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
