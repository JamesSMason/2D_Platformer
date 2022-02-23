using UnityEngine;
using UnityEngine.UI;

public class LivesUI : MonoBehaviour
{
    [SerializeField] GameObject livesPanel = null;
    [SerializeField] GameObject lifePrefab = null;

    LazyValue<GameState> gameState = null;

    void Awake()
    {
        gameState = new LazyValue<GameState>(GetGameState);
    }

    void Start()
    {
        gameState.ForceInit();
        UpdateLivesUI();
    }

    void OnEnable()
    {
        if (gameState.value != null)
        {
            gameState.value.OnLivesChanged += UpdateLivesUI;
        }
    }

    void OnDisable()
    {
        if (gameState.value != null)
        {
            gameState.value.OnLivesChanged -= UpdateLivesUI;
        }
    }

    private GameState GetGameState()
    {
        return FindObjectOfType<GameState>();
    }

    private void UpdateLivesUI()
    {
        Image[] lives = livesPanel.GetComponentsInChildren<Image>();
        foreach (Image life in lives)
        {
            Destroy(life.gameObject);
        }

        if (gameState.value == null) { return; }
        for (int i = 0; i < gameState.value.GetLives(); i++)
        {
            Instantiate(lifePrefab, livesPanel.transform);
        }
    }
}