using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    bool hitHazard = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Hazard") || hitHazard) { return; }
        hitHazard = true;
        FindObjectOfType<GameState>().LoseLife();
    }
}