using System;
using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float levelTimeInSeconds = 180.0f;
    [SerializeField] int scorePerAirUnit = 10;
    [SerializeField] float delay = 0.01f;

    GameState gameState;

    bool isGameLive = true;
    float currentTime = 0.0f;

    public Action OnSliderChanged;

    void Awake()
    {
        gameState = FindObjectOfType<GameState>();
    }

    void Update()
    {
        if (isGameLive)
        {
            currentTime += Time.deltaTime;
            if (OnSliderChanged != null)
            {
                OnSliderChanged();
            }
            if (currentTime > levelTimeInSeconds)
            {
                gameState.LoseLife();
            }
        }
    }

    public float GetNormalisedTime()
    {
        return 1 - (currentTime / levelTimeInSeconds);
    }

    public IEnumerator ConvertAirToScore()
    {
        isGameLive = false;
        float timeRemaining = levelTimeInSeconds - currentTime;
        for (float i = 0; i < timeRemaining; i++)
        {
            gameState.IncreaseScore(scorePerAirUnit);
            currentTime++;
            if (OnSliderChanged != null)
            {
                OnSliderChanged();
            }
            yield return new WaitForSeconds(delay);
        }
        FindObjectOfType<LevelLoader>().LoadNextLevel();
    }
}