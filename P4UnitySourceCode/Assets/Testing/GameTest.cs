using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
using System;

public class GameTest
{
    [Test]
    public void GameTestSimplePasses()
    {

    }


    [UnityTest]
    public IEnumerator Test1_PlaySceneName()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadSceneAsync("PlayScene");//Beginning testing by checking initial scenes name

        yield return new WaitForSeconds(1f);
        Assert.AreEqual("PlayScene", SceneManager.GetActiveScene().name);
    }

    [UnityTest]
    public IEnumerator Test2_TutorialSceneTest()
    {
        yield return new WaitForSeconds(1f);
        GameObject playButton = GameObject.Find("TutorialButton"); //Finding Button object to simulate click on

        Assert.IsNotNull(playButton, "Play button not found in PlayScene scene");
        playButton.GetComponent<Button>().onClick.Invoke(); //Simulating Click

        yield return new WaitForSeconds(2f);
        Assert.AreEqual("Tutorial", SceneManager.GetActiveScene().name, "Game scene not loaded after clickingTutorial button");//Assert to check scene change
    }

    [UnityTest]
    public IEnumerator Test3_ReturnToMainMenuTest()
    {
        yield return new WaitForSeconds(1f);
        GameObject playButton = GameObject.Find("ReturnButton"); //Finding Button object to simulate click on

        Assert.IsNotNull(playButton, "Return button not found in Tutorial scene");
        playButton.GetComponent<Button>().onClick.Invoke();//Simulating Click

        yield return new WaitForSeconds(2f);
        Assert.AreEqual("PlayScene", SceneManager.GetActiveScene().name, "Game scene not loaded after clicking Return button"); //Assert to check scene change
    }

    [UnityTest]
    public IEnumerator Test4_BeginGameTest()
    {
        yield return new WaitForSeconds(1f);
        GameObject playButton = GameObject.Find("PlayButton"); //Finding Button object to simulate click on

        Assert.IsNotNull(playButton, "Play button not found in PlayScene scene");
        playButton.GetComponent<Button>().onClick.Invoke(); //Simulating Click

        yield return new WaitForSeconds(2f);
        Assert.AreEqual("CharacterSelectScene", SceneManager.GetActiveScene().name, "Game scene not loaded after clicking the Play button"); //Assert to check scene change
    }

    [UnityTest]
    public IEnumerator Test5_CharacterSelectToMatchingSceneTest()
    {
        yield return new WaitForSeconds(1f);
        GameObject playButton = GameObject.Find("SaulButtonGoToCMS");//Finding Button object to simulate click on

        Assert.IsNotNull(playButton, "Play button not found in Character Select scene");
        playButton.GetComponent<Button>().onClick.Invoke();//Simulating Click

        yield return new WaitForSeconds(2f);
        Assert.AreEqual("CardMatchingScene", SceneManager.GetActiveScene().name, "Game scene not loaded after clicking the Choose Character button"); //Assert to check scene change
    }


    [UnityTest]
    public IEnumerator Test6_CorrectAnswerInteractions()
    {
        yield return new WaitForSeconds(1f); 
        string[] correctAnswers = {"Persecuted Christians", "Was highly educated", "Was a Pharisee" }; //Array of strings to check for the correct text (answers)
        Tile[] allTiles = GameObject.FindObjectsOfType<Tile>();//finding all tiles with a text child object

        foreach (string correctAnswer in correctAnswers)//foreach loop to iterate over the tiles to find the correct text
        {
            Tile correctTile = allTiles.FirstOrDefault(tile => tile.textComponent.text == correctAnswer); // Finding the Tile's text component with the correct answer utilizing LINQ method  FirsOrDefault
            Assert.IsNotNull(correctTile, "Tile with correct answer not found for " + correctAnswer);

            correctTile.OnMouseDown();
            yield return new WaitForSeconds(1f);
        }

        yield return new WaitForSeconds(1f);
        Assert.AreEqual(30, EndGameScoreManager.endGameScore, "The score did not correctly update to 30 after all correct answers were selected.");
        //Final Assert: Checking endGameScore is equal to 30 because each answer gives 10 points with a total of 3 correct answers.
    }
    [UnityTest]
    public IEnumerator Test7_MatchingGametoEndGame()
    {
        yield return new WaitForSeconds(1f);
        GameObject playButton = GameObject.Find("EG Button");//Finding Button object to simulate click on

        Assert.IsNotNull(playButton, "Go to High Scores button not found in Character Select scene");
        playButton.GetComponent<Button>().onClick.Invoke();//Simulating Click

        yield return new WaitForSeconds(2f);
        Assert.AreEqual("EndGameScene", SceneManager.GetActiveScene().name, "Game scene not loaded after clicking the End Game Button");//Assert to check scene change
    }
    [UnityTest]
    public IEnumerator Test8_EndGameToMainMenu()
    {
        yield return new WaitForSeconds(1f);
        GameObject playButton = GameObject.Find("returnmainmenu");//Finding Button object to simulate click on

        Assert.IsNotNull(playButton, "Go to Main Menu button not found in Character Select scene");
        playButton.GetComponent<Button>().onClick.Invoke();//Simulating Click

        yield return new WaitForSeconds(2f);
        Assert.AreEqual("PlayScene", SceneManager.GetActiveScene().name, "Main Menu not loaded after clicking the returnmainmenu button");//Assert to check scene change
    }

}

    