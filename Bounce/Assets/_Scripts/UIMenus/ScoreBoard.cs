using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
// calculates and displays scoreboard in levelcompletion menu
public class ScoreBoard : MonoBehaviour
{
    public TextMeshProUGUI scoreboardText;
    public string levelTimeString;
    public float totalDamageTaken;
    public float totalMagikaUsed;
    public int closeCalls;
    public float finalLevelScore;

    public int pointsPerSecondTimer;
    public int pointsPerDamageTaken;
    public int pointsPerManaUsed;
    public int pointsPerCloseCall;
    public Game_Timer gameTimer;
    public Unit playerStats;

    public void Update()
    {
        if(GameObject.FindGameObjectWithTag("Player")!=null)
        {
            playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<Unit>();
        }
    }
    public void CalculateScoreBoard()
    {
        levelTimeString = gameTimer.GetTime();
        int maxLevelPoints = 0;
        string[] timeComponents = levelTimeString.Substring(13).Split(':');
        int minutes = int.Parse(timeComponents[0]);
        int seconds = int.Parse(timeComponents[1]);
        int milliseconds = int.Parse(timeComponents[2]);
        float levelTime = minutes * 60f + seconds + milliseconds / 600f;
        totalDamageTaken = playerStats.totalDamageTaken;
        totalMagikaUsed = playerStats.totalMagikaUsed;
        closeCalls = playerStats.closeCalls;
        finalLevelScore = Mathf.Floor(maxLevelPoints + (levelTime * pointsPerSecondTimer ) + (totalDamageTaken * pointsPerDamageTaken) + (totalMagikaUsed * pointsPerManaUsed) + (closeCalls * pointsPerCloseCall));
    }
    public void DisplayScoreBoard()
    {
        CalculateScoreBoard();
        string finalTime = levelTimeString + "\n";
        string goldCollected = playerStats.goldCollected.ToString() + "\n";
        string damageTaken = totalDamageTaken.ToString() + "\n";
        string manaUsed = totalMagikaUsed.ToString() + "\n";
        string closeCallsString = closeCalls.ToString() + "\n";
        string totalScore = finalLevelScore.ToString() + "\n";
        scoreboardText.text = finalTime + goldCollected + damageTaken + manaUsed + closeCallsString + totalScore;
    }
}
