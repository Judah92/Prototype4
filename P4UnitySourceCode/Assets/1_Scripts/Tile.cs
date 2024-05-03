using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Presets;
using UnityEngine;
using static UnityEngine.ParticleSystem;
using UnityEngine.UI;
using System;
using System.Linq;//needed this (Language Integrated Query) to call .Any


public class Tile : MonoBehaviour
{
    public Sprite originalSprite;
    public Text textComponent;
    public bool IsCorrectAnswer = false;//bool for checking tile answer
    public ManageCards manageCards; //allows me to access the ManageCards function through the gameManager object
    //public int trackScore = 0;
    //public Text CardsScore;
    //public string filePath = "Assets/CardScores.txt";



    void Start()
    {
        manageCards = FindObjectOfType<ManageCards>();

        if (manageCards == null)
        {
            Debug.LogError("ManageCards instance not found.");
        }

        Canvas childCanvas = GetComponentInChildren<Canvas>();
        if (childCanvas != null)
        {
            textComponent = childCanvas.GetComponentInChildren<Text>();
        }


    }

    void Update()
    {
        
    }

    public void OnMouseDown()
    {
        string trimmedText = textComponent.text.Trim(); //removes leading and trailing whitespace
        bool isCorrect = LevelManager.Instance.correctAnswers.Any(answer => string.Equals(answer, trimmedText, StringComparison.OrdinalIgnoreCase));
        //LevelManager.Instance.CorrectAnswers is singleton (static) - .Any (answer => string.Equals) checkes each answer trimming the leading and trailing whitespaces, and using a case-insensitive string comparison
        
        //Checking if clicked text is the correct answer
        if (isCorrect)
        {
            //Debug.Log("Correct");//Testing Console answer clicks
            manageCards.ShowCorrectAnswerPopup(); // Show the "correct" popup
            manageCards.AddScore(10); //adding 10 points to displayed score
        }
        else
        {
            //Debug.Log("Wrong");//Testing Console answer clicks
            manageCards.ShowWrongAnswerPopup(); // Show the "wrong" popup
        }

    }

    public void SetOriginalSprite(Sprite newSprite) 
    { 
        GetComponent<SpriteRenderer>().sprite = newSprite;
    }

    public void SetText(string text)//setting text component on tile
    {
        if (textComponent != null)
        {
            textComponent.text = text;
        }
    }
}
