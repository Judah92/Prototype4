using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;//delcaring static instance to be used with other scripts
    public string sceneName;
    public string selectedCharacter;
    public List<string> correctAnswers;

    void Awake() 
    {
        Instance = this;
        DontDestroyOnLoad(this.gameObject);//Changed to this - having else if when the Instance never goes null caused being stuck on character selection.
        /*if (Instance == null)//Ensuring the GameObject (Saul correct answers persists through scenes)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }*/
    }

    public void SetCharacterAndAnswers(string character, List<string> answers)
    {
        selectedCharacter = character; //setting Name and Correct answers 
        correctAnswers = answers;

    }
    public void StartGameWithSaul()//name and answers for Saul
    {
        SetCharacterAndAnswers("Saul of Tarsus", new List<string> {
            "Persecuted Christians",
            "Was highly educated",
            "Was a Pharisee"});
        ChangeScene();
    }
    public void ChangeScene()//Passing string to game object to be used in field
    {
        SceneManager.LoadScene("CardMatchingScene");
    }
}
