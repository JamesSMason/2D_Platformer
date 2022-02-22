using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText = null;
    GameState gameState = null;

    void Start()
    {
        gameState = FindObjectOfType<GameState>();
    }

    void Update()
    {
        scoreText.text = gameState.GetScore().ToString();
    }
}