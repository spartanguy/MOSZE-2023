using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreboardManager : MonoBehaviour
{   
    public int scoreNumber = 0;
    public TMP_Text scoreBoardText;

    //Ez a függvény frissíti a játékos pontszámát
    void Update()
    {
        scoreNumber = Game.Instance.GetScore();
        DisplayScore(scoreNumber);
    }
    //Ez a funkció írja ki a canvasre a játékos pontszámát
    void DisplayScore(int scoreToDisplay) {

        scoreBoardText.text = scoreNumber.ToString();
    }
}
