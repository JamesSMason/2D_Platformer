using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour
{
    [SerializeField] Slider slider = null;
    [SerializeField] float levelTimeInSeconds = 180.0f;

    float currentTime = 0.0f;

    void Awake()
    {
        slider.value = 1.0f;
    }

    void Update()
    {
        currentTime += Time.deltaTime;
        slider.value = 1 - (currentTime / levelTimeInSeconds);
    }

    public void OutOfAir()
    {
        if (slider.value <= Mathf.Epsilon)
        {
            FindObjectOfType<GameState>().LoseLife();
        }
    }
}