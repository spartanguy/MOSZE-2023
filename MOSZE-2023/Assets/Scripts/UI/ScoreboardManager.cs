using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreboardManager : MonoBehaviour
{   
    public int scoreNumber = 0;
    public TMP_Text scoreBoardText;

    void Update()
    {
        scoreNumber = Game.Instance.GetScore();
        DisplayScore(scoreNumber);
    }

    void DisplayScore(int scoreToDisplay) {

        scoreBoardText.text = scoreNumber.ToString();
    }
}
