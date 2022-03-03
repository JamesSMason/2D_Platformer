using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MM.Core
{
    public class LevelLoader : MonoBehaviour
    {
        [SerializeField] float loadDelayInSeconds = 0.0f;
        [SerializeField] float nextLevelDelayInSeconds = 3.0f;
        [SerializeField] int firstLevelIndex = 2;
        [SerializeField] int gameOverIndex = 1;

        public void LoadMainMenu()
        {
            SceneManager.LoadScene(0);
        }

        public void NewGame()
        {
            SceneManager.LoadScene(firstLevelIndex);
        }

        public void LoadNextLevel()
        {
            int nextLevelID = SceneManager.GetActiveScene().buildIndex + 1;
            if (nextLevelID >= SceneManager.sceneCountInBuildSettings)
            {
                nextLevelID = firstLevelIndex;
            }
            StartCoroutine(DelayLoad(nextLevelID, nextLevelDelayInSeconds));
        }

        public void RestartLevel()
        {
            StartCoroutine(DelayLoad(SceneManager.GetActiveScene().buildIndex, loadDelayInSeconds));
        }

        public void LoadGameOver()
        {
            StartCoroutine(DelayLoad(gameOverIndex, loadDelayInSeconds));
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        private IEnumerator DelayLoad(int buildIndex, float delay)
        {
            yield return new WaitForSeconds(delay);
            SceneManager.LoadScene(buildIndex);
        }
    }
}