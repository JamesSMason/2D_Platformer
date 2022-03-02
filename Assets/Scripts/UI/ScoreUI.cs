using MM.Core;
using TMPro;
using UnityEngine;

namespace MM.UI
{
    public class ScoreUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI scoreText = null;
        [SerializeField] TextMeshProUGUI highScoreText = null;
        LazyValue<GameState> gameState = null;

        void Awake()
        {
            gameState = new LazyValue<GameState>(GetGameState);
        }

        void Start()
        {
            highScoreText.text = $"{gameState.value.GetHighScore():000000}";
            UpdateScoreUI();
        }

        void OnEnable()
        {
            gameState.value.OnScoreChanged += UpdateScoreUI;
        }

        void OnDisable()
        {
            gameState.value.OnScoreChanged -= UpdateScoreUI;
        }

        private GameState GetGameState()
        {
            return FindObjectOfType<GameState>();
        }

        private void UpdateScoreUI()
        {
            scoreText.text = $"{gameState.value.GetScore():000000}";
        }
    }
}