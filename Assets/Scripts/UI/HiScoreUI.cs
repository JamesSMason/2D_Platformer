using UnityEngine;
using TMPro;
using MM.Core;

namespace MM.UI
{
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
                gameState.SaveGame();
            }
        }
    }
}