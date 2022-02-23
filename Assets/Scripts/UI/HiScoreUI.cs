using UnityEngine;
using TMPro;

public class HiScoreUI : MonoBehaviour
{
    GameState gameState = null;
    void Awake()
    {
        gameState = FindObjectOfType<GameState>();
    }

    void Start()
    {
        if (gameState.GetScore() > gameState.GetHighScore())
        {
            gameState.SetHighScore(gameState.GetScore());
            GetComponent<TextMeshProUGUI>().text = $"You achieved a new High Score of {gameState.GetHighScore()}!";
            LevelLoader levelLoader = FindObjectOfType<LevelLoader>();
            if (levelLoader == null)
            {
                Debug.Log("nope. not here.");
            }
            else
            {
                levelLoader.SaveGame();
            }
        }
    }
}