using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Game_Timer : MonoBehaviour
{
    private bool timerActive = true;
    public TextMeshProUGUI gameTimeText;
    private float elapsedSeconds = 0f;

    void Update()
    {
        if (timerActive == true)
        {
            elapsedSeconds += Time.deltaTime;
            TimeSpan time = TimeSpan.FromSeconds(elapsedSeconds);
            string minutes = time.Minutes.ToString();
            string seconds = time.Seconds.ToString("00");
            string milliseconds = Mathf.FloorToInt(time.Milliseconds / 10f).ToString("00");
            gameTimeText.text = "<mspace=37px>" + minutes + ":" + seconds + ":" + milliseconds;
        }
    }

    public void StartTimer()
    {
        timerActive = true;
    }

    public void StopTimer()
    {
        timerActive = false;
    }

    public string GetTime()
    {
        return gameTimeText.text;
    }

    public void ResetTimer()
    {
        elapsedSeconds = 0f;
        gameTimeText.text = "<mspace=37px>00:00:00";
    }
}
