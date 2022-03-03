using MM.Core;
using UnityEngine;

namespace MM.UI
{
    public class MenuUI : MonoBehaviour
    {
        GameState gameState = null;

        void Awake()
        {
            gameState = FindObjectOfType<GameState>();
        }

        public void StartNewGame()
        {
            gameState.NewGame();
        }

        public void QuitGame()
        {
            gameState.QuitGame();
        }

        public void LoadMainMenu()
        {
            gameState.LoadMainMenu();
        }
    }
}