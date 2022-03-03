using MM.Saving;
using UnityEngine;

namespace MM.Core
{
    public class Score : MonoBehaviour, ISaveable
    {
        int score = 0;
        int highScore = 0;

        public int GetScore()
        {
            return score;
        }

        public int GetHighScore()
        {
            return highScore;
        }

        public void SetHighScore(int highScore)
        {
            this.highScore = highScore;
        }

        public void SetScore(int pointsValue)
        {
            score = pointsValue;
        }

        public void ResetScore()
        {
            SetScore(0);
            SetHighScore(0);
        }

        public object CaptureState()
        {
            return highScore;
        }

        public void RestoreState(object state)
        {
            SetHighScore((int)state);
        }
    }
}