using UnityEngine;

public class HazardDetector : MonoBehaviour
{
    bool hitHazard = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player") || hitHazard) { return; }
        hitHazard = true;
        FindObjectOfType<GameState>().LoseLife();
    }
}