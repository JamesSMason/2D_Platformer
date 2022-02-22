using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    CapsuleCollider2D myCapsuleCollider = null;

    void Awake()
    {
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Hazard")) { return; }
        FindObjectOfType<GameState>().LoseLife();
    }
}