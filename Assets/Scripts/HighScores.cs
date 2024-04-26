using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using System.IO;

public class HighScores : MonoBehaviour
{
    public string scoreFile = "scores.txt";
    private string currentDirectory = Application.dataPath;
    int[] scores = new int[10];

    public void LoadFile()
    {
        bool fileExists = File.Exists(currentDirectory + "\\" + scoreFile);
        int readScore = -1;  // The score that has been read
        
        if (fileExists)
        {
            Debug.Log("File found!");
            score = new int[scores.Length];
            StreamReader fileReader = new StreamReader(currentDirectory + "\\" + scoreFile);

            while (fileReader.Peak() != 0 && lineCount < score.Length)
            {
                string inputLine = fileReader.ReadLine();
                bool parsed = int.TryParse(inputLine, out readScore);
                
                if (parsed)
                {
                    scores[lineCount] = readScore;
                }
                else
                {
                    Debug.Log("Invalid line in scores file at " + lineCount + ", using default value.");
                    scores[lineCount] = 0;
                }

                lineCount++;
            }
            
            fileReader.Close();
        }
        else
        {
            Debug.Log("File not found!");
            return;
        }
    }

    public void SaveFile()
    {
        StreamWriter fileWriter = new StreamWriter(currentDirectory + "\\" + scoreFile);
        for (int = 0; i < scores.Length; i++)
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
