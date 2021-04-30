using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    public enum Score
    {
        AiScore, PlayerScore
    }
    public Text AiScoreText, PlayerScoreText;
    public UIManager uIManager;
    public AirHockeyPlayerMovement airHockeyPlayer;

    #region Scores
        public int MaxScore, aiScore, playerScore;
        private int AiScore { 
            get{
                return aiScore;
            }   
            set {
                aiScore = value;    
                if (value == MaxScore)
                {
                    uIManager.ShowRestartCanvas(true);
                }
            }
        }
        private int PlayerScore { 
            get{
                return playerScore;
            }   
            set {
                playerScore = value;    
                if (value == MaxScore)
                {
                    uIManager.ShowRestartCanvas(false);
                }
            }
        }
    #endregion

    public void Increment(Score whichScore){
        if (whichScore == Score.AiScore)
        {
            AiScoreText.text = (++AiScore).ToString();
        }else
        {
            PlayerScoreText.text = (++PlayerScore).ToString();
        }
    }
    public void ResetScores(){
        AiScore = PlayerScore = 0;
        AiScoreText.text = PlayerScoreText.text = "0";
    }
}
