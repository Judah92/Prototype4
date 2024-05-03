using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EndGameScoreManager : MonoBehaviour
{
    public static int endGameScore = 0;//Had to change this to a static int instead of using an Instance of EndGameScoreManager or else data would be lost when transitioning to endgamescene <-- FIGURING THIS OUT CAUSED ME MUCH UNEEDED DEBUGGING PAIN
    public void Start()
    {
        
    }


    public void AddScore(int score)
    {
        endGameScore = endGameScore + score; //Tracking total score

    }


    // 
    // public void ResetScore()
    // {
    //     endGameScore = 0;
    // }
}


