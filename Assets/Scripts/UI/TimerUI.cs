using MM.Core;
using UnityEngine;
using UnityEngine.UI;

namespace MM.UI
{
    public class TimerUI : MonoBehaviour
    {
        [SerializeField] Slider slider = null;

        GameState gameState = null;

        void Awake()
        {
            gameState = FindObjectOfType<GameState>();
        }

        void Start()
        {
            UpdateSliderUI();
        }

        void OnEnable()
        {
            if (gameState != null)
            {
                gameState.OnSliderChanged += UpdateSliderUI;
            }
        }

        void OnDisable()
        {
            if (gameState != null)
            {
                gameState.OnSliderChanged -= UpdateSliderUI;
            }
        }

        private void UpdateSliderUI()
        {
            slider.value = gameState.GetNormalisedTime();
        }
    }
}