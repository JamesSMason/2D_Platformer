using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour
{
    [SerializeField] Slider slider = null;

    Timer timer = null;

    void Awake()
    {
       timer = FindObjectOfType<Timer>();
    }

    void Start()
    {
        UpdateSliderUI();
    }

    void OnEnable()
    {
        if (timer != null)
        {
            timer.OnSliderChanged += UpdateSliderUI;
        }
    }

    void OnDisable()
    {
        if (timer != null)
        {
            timer.OnSliderChanged -= UpdateSliderUI;
        }
    }

    private void UpdateSliderUI()
    {
        slider.value = timer.GetNormalisedTime();
    }
}