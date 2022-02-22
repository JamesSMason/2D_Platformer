using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] float loadDelayInSeconds = 2.0f;

    public void NewGame()
    {
        GameState gameState = FindObjectOfType<GameState>();
        gameState.ResetGame();
        SceneManager.LoadScene(1);
    }

    public void LoadNextLevel()
    {
        int nextLevelID = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextLevelID >= SceneManager.sceneCountInBuildSettings)
        {
            nextLevelID = 0;
        }
        StartCoroutine(DelayLoad(nextLevelID));
    }

    public void RestartLevel()
    {
        StartCoroutine(DelayLoad(SceneManager.GetActiveScene().buildIndex));
    }

    public void RestartGame()
    {
        StartCoroutine(DelayLoad(0));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private IEnumerator DelayLoad(int buildIndex)
    {
        yield return new WaitForSeconds(loadDelayInSeconds);
        SceneManager.LoadScene(buildIndex);
    }
}