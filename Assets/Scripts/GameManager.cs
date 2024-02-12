using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public TextMeshProUGUI scoreText;
    private int score = 0;
    
    private float timeElapsed = 0f;
    public TextMeshProUGUI timerText;
    private bool timerRunning = true;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        BoxManager.ResetBoxState();
        UpdateScore(0);
    }
    
    private void Update()
    {
        if (timerRunning)
        {
            timeElapsed += Time.deltaTime;
            DisplayTime(timeElapsed);
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = score.ToString("00");
    }
    
    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    
    public void StopTimer()
    {
        timerRunning = false;
    }
}
