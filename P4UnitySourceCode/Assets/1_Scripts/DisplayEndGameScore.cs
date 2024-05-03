using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using System.IO;

public class DisplayEndGameScore : MonoBehaviour
{
    public Text EndingScoreText;//text object for displaying the score
    public string filePath = "Assets/CardScores.txt";//file path for external .txt file

    private void Start()
    {
        EndingScoreText = FindObjectOfType<Text>();//Creating score to be held in inspector
        EndingScoreText.text = " " + EndGameScoreManager.endGameScore.ToString();//Displaying the score
    }
    void OnDisable()
    {
        SaveScoreToFile(EndGameScoreManager.endGameScore, filePath);//Writing to file when game is disabled/stoped playing
    }
    void SaveScoreToFile(int score, string filePath) //Function to write to file taking the score and filePath as params
    {
        try
        {
            File.WriteAllText(filePath, "Your Final Score: " + score.ToString());//writing to the file
        }
        catch 
        {      
            Debug.LogError("Couldn't write to file");//catch the exception if an error occurs.
        }
    }
}



