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

    private LevelLoader GetLevelLoader()
    {
        return FindObjectOfType<LevelLoader>();
    }
}