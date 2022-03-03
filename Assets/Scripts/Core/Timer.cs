using UnityEngine;

namespace MM.Core
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] float levelTimeInSeconds = 180.0f;

        float currentTime = 0.0f;

        public float GetCurrentTime()
        {
            return currentTime;
        }

        public float GetLevelTime()
        {
            return levelTimeInSeconds;
        }

        public void UpdateTimer(float value)
        {
            currentTime += value;
        }

        public float GetNormalisedTime()
        {
            return 1 - (currentTime / levelTimeInSeconds);
        }
    }
}