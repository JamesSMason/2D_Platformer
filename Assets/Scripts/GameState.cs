using UnityEngine;

public class GameState : MonoBehaviour
{
    [SerializeField] int numberOfLives = 3;
    [SerializeField] int scoreForExtraLife = 20000;

    int score;
    int livesRemaining;

    private void Start()
    {
        ResetGame();
    }

    public int GetScore()
    {
        return score;
    }

    public void IncreaseScore(int pointsValue)
    {
        int newScore = score + pointsValue;
        Debug.Log($"{score} + {pointsValue} = {newScore}");
        if (newScore > scoreForExtraLife && score < scoreForExtraLife)
        {
            GainLife();
        }
        score = newScore;
    }

    public void LoseLife()
    {
        livesRemaining--;
        LevelLoader levelLoader = FindObjectOfType<LevelLoader>();
        if (livesRemaining <= 0)
        {
            levelLoader.RestartGame();
        }
        else
        {
            levelLoader.RestartLevel();
        }
    }

    public void GainLife()
    {
        livesRemaining++;
    }

    public void ResetGame()
    {
        livesRemaining = numberOfLives;
        score = 0;
    }
}