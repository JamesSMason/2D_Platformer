using MM.Core;
using UnityEngine;

namespace MM.Environment
{
    public class HazardDetector : MonoBehaviour
    {
        bool hitHazard = false;

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (!collision.gameObject.CompareTag("Player") || hitHazard) { return; }
            hitHazard = true;
            FindObjectOfType<GameState>().LoseLife();
        }
    }
}