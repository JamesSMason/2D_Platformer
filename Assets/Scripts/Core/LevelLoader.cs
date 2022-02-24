using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] float loadDelayInSeconds = 0.0f;
    [SerializeField] int firstLevelIndex = 2;
    [SerializeField] int gameOverIndex = 1;

    const string saveFile = "save";

    SavingSystem savingSystem = null;

    void Awake()
    {
        savingSystem = FindObjectOfType<SavingSystem>();
    }

    public void SaveGame()
    {
        savingSystem.Save(saveFile);
    }

    public void LoadGame()
    {
        savingSystem.Load(saveFile);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void NewGame()
    {
        GameState gameState = FindObjectOfType<GameState>();
        gameState.ResetGame();
        LoadGame();
        SceneManager.LoadScene(firstLevelIndex);
    }

    public void LoadNextLevel()
    {
        int nextLevelID = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextLevelID >= SceneManager.sceneCountInBuildSettings)
        {
            nextLevelID = firstLevelIndex;
        }
        StartCoroutine(DelayLoad(nextLevelID));
    }

    public void RestartLevel()
    {
        StartCoroutine(DelayLoad(SceneManager.GetActiveScene().buildIndex));
    }

    public void LoadGameOver()
    {
        StartCoroutine(DelayLoad(gameOverIndex));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private IEnumerator DelayLoad(int buildIndex)
    {
        yield return new WaitForSeconds(loadDelayInSeconds);
        FindObjectOfType<GameState>().SetPlayGame(true);
        SceneManager.LoadScene(buildIndex);
    }
}