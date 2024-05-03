using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class ManageCards : MonoBehaviour
{
    public GameObject card;
    public GameObject textPrefab;
    public string[] cardTexts;
    public GameObject correctAnswerPopup;
    public GameObject wrongAnswerPopup;
    //public int trackScore = 0;
    public Text CardsScore;
    
    void Start() 
    { 
        cardTexts = new string[6]; //instantiating an array and appending the Strings to indices - can be added to later
        cardTexts[0] = "Persecuted Christians";
        cardTexts[1] = "Was highly educated";
        cardTexts[2] = "Was a Pharisee";
        cardTexts[3] = "Baptized the Ethiopian Eunich";
        cardTexts[4] = "Said render unto Caesar what is Caesars";
        cardTexts[5] = "Rode a horse into battle";
        displayCards();//calling displayCards to ensure tile and string generation occurs at runtime to ensure correct responses from clicks.
        DisplayScore();//same with DisplayScore
    }
    public void AddScore(int score)//function to add score called by Tile.cs
    {
        //trackScore = trackScore + score;
        EndGameScoreManager.endGameScore += score;
        DisplayScore();
    }
    public void DisplayScore() 
    {
        //CardsScore.text = "Score: " + trackScore;
        CardsScore.text = "Score: " + EndGameScoreManager.endGameScore.ToString();//Displaying score on CardScore.text object via inspector assignment
    }
    public void displayCards()//Dynamically generating the tiles and the strings
    {
        for (int i = 0; i < cardTexts.Length; i++)
        {
            addACard(i, cardTexts[i]);//Passing the tiles and strings to AddACard
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ShowCorrectAnswerPopup()
    {
        correctAnswerPopup.SetActive(true);
        Invoke("HideCorrectAnswerPopup", 2f);//Calling (invoke) on the HideCorrectAnswerPopup with a 2 second delay
    }
    public void ShowWrongAnswerPopup()
    {
        wrongAnswerPopup.SetActive(true);
        Invoke("HideWrongAnswerPopup", 2f);//Calling (invoke) on the HideWrongAnswerPopup with a 2 second delay
    }

    void HideCorrectAnswerPopup()
    {
        correctAnswerPopup.SetActive(false);
    }
    void HideWrongAnswerPopup()
    {
        wrongAnswerPopup.SetActive(false);
    }

    void addACard(int rank, string text)//Dynamically generating tiles, child canvas and it's text and the strings passed from DisplayCards
    {
     float cardOriginalScale = card.transform.localScale.x; 
     float scaleFactor = (400 * cardOriginalScale) / 100.0f;
     float rightOffset = 1.0f;
     GameObject centerOfScreen = GameObject.Find("centerOfScreen");
     Vector3 newPosition = new Vector3(centerOfScreen.transform.position.x + ((rank - 3) * scaleFactor) + rightOffset, centerOfScreen.transform.position.y, centerOfScreen.transform.position.z); // Adjusted for centering

     GameObject c = Instantiate(card, newPosition, Quaternion.identity);
     c.tag = "" + rank;
        
        Tile tileComponent = c.GetComponent<Tile>();
        if (tileComponent != null)
        {
            tileComponent.SetText(text); //setting text on the Tile
        }
        
     //setting up the canvas to be displayed on the cards
     GameObject canvasObject = new GameObject("CardCanvas"); //Creating Canvas as a child of the cloned tiles for displaying text
     canvasObject.transform.SetParent(c.transform, false);
     Canvas canvas = canvasObject.AddComponent<Canvas>();
     canvas.renderMode = RenderMode.WorldSpace;//Had to use WorldSpace as the render mode, Overlay didn't work well

     CanvasScaler cs = canvasObject.AddComponent<CanvasScaler>();//creating the canvas scaler
     cs.dynamicPixelsPerUnit = 10;//setting the number of pixels for the generated text
     RectTransform rect = canvasObject.GetComponent<RectTransform>();

     rect.sizeDelta = new Vector2(100, 100); //x,y Setting the Width and Height of the Canvas
     rect.localPosition = new Vector3(0, 0, 0);//Setting canvas to be centered on the parent Tile
     rect.localScale = new Vector3(0.02f, 0.02f, 1f);//x, y, z Setting the scale of canvas to parent Tile to 2% shrinking the canvas.


        //Setting up the text and font to be displayed in the scene
     GameObject textObject = new GameObject("CardText"); //creating the text object to be used on the canvas
     textObject.transform.SetParent(canvasObject.transform, false);//Setting Canvas as the texts parent object
     Text textComponent = textObject.AddComponent<Text>();
     textComponent.text = text;//setting up the font
     textComponent.font = Font.CreateDynamicFontFromOSFont("Arial", 14);
     textComponent.fontSize = 15; 
     textComponent.color = Color.red;

     textComponent.alignment = TextAnchor.MiddleCenter;//centering the text on the Canvas
     textComponent.horizontalOverflow = HorizontalWrapMode.Wrap; // Ensure text wraps in the canvas
     textComponent.verticalOverflow = VerticalWrapMode.Overflow; // Allow text to overflow vertically out of the canvas (if needed eventually)

     RectTransform textRectTransform = textObject.GetComponent<RectTransform>();
     textRectTransform.sizeDelta = new Vector2(90, 100); // x,y 
     textRectTransform.localPosition = new Vector3(0, 0, 0); //x,y,z

     bool isCorrectAnswer = LevelManager.Instance.correctAnswers.Contains(text);//checking the list in LevelManager correctAnswers
     c.GetComponent<Tile>().IsCorrectAnswer = isCorrectAnswer;//checking for correct answers
    }
}



