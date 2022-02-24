using UnityEngine;

public class MenuUI : MonoBehaviour
{
    LazyValue<LevelLoader> levelLoader = null;

    void Awake()
    {
        levelLoader = new LazyValue<LevelLoader>(GetLevelLoader);
    }

    public void StartNewGame()
    {
        levelLoader.value.NewGame();
    }

    public void QuitGame()
    {
        levelLoader.value.QuitGame();
    }

    public void LoadMainMenu()
    {
        levelLoader.value.LoadMainMenu();
    }

    private LevelLoader GetLevelLoader()
    {
        return FindObjectOfType<LevelLoader>();
    }
}