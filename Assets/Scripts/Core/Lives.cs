using UnityEngine;

namespace MM.Core
{
    public class Lives : MonoBehaviour
    {
        [SerializeField] int numberOfLives = 3;
        int livesRemaining;

        public int GetLivesRemaining()
        {
            return livesRemaining;
        }

        public void SetLivesRemaining(int value)
        {
            livesRemaining = value;
        }

        public void ResetLives()
        {
            livesRemaining = numberOfLives;
        }
    }
}