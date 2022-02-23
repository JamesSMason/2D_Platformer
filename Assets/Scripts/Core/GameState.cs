using System;
using UnityEngine;

public class GameState : MonoBehaviour, ISaveable
{
    [SerializeField] int numberOfLives = 3;
    [SerializeField] int scoreForExtraLife = 20000;

    int score;
    int livesRemaining;
    int highScore;

    public Action OnScoreChanged;
    public Action OnLivesChanged;

    private void Start()
    {
        ResetGame();
    }

    public int GetScore()
    {
        return score;
    }

    public int GetHighScore()
    {
        return highScore;
    }

    public int GetLives()
    {
        return livesRemaining;
    }

    public void SetHighScore(int highScore)
    {
        this.highScore = highScore;
    }

    public void IncreaseScore(int pointsValue)
    {
        int newScore = score + pointsValue;
        if (newScore > scoreForExtraLife && score < scoreForExtraLife)
        {
            GainLife();
        }
        score = newScore;
        if (OnScoreChanged != null)
        {
            OnScoreChanged();
        }
    }

    public void LoseLife()
    {
        livesRemaining--;
        if (OnLivesChanged != null)
        {
            OnLivesChanged();
        }
        LevelLoader levelLoader = FindObjectOfType<LevelLoader>();
        if (livesRemaining <= 0)
        {
            levelLoader.LoadGameOver();
        }
        else
        {
            levelLoader.RestartLevel();
        }
    }

    public void GainLife()
    {
        livesRemaining++;
        if (OnLivesChanged != null)
        {
            OnLivesChanged();
        }
    }

    public void ResetGame()
    {
        livesRemaining = numberOfLives;
        score = 0;
        if (OnLivesChanged != null)
        {
            OnLivesChanged();
        }
        if (OnScoreChanged != null)
        {
            OnScoreChanged();
        }
    }

    public object CaptureState()
    {
        return highScore;
    }

    public void RestoreState(object state)
    {
        highScore = (int)state;
    }
}