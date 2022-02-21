using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTimer : MonoBehaviour
{
    [SerializeField] Slider slider = null;
    [SerializeField] float levelTimeInSeconds = 180.0f;

    float currentTime = 0.0f;

    void Awake()
    {
        slider.value = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        slider.value = 1 - (currentTime / levelTimeInSeconds);
    }

    public void OutOfAir()
    {
        if (slider.value <= Mathf.Epsilon)
        {
            Time.timeScale = 0.0f;
            Debug.Log("Out of Air!!");
        }
    }
}
