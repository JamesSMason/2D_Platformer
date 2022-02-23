using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    BoxCollider2D myBoxCollider = null;
    PlayerController playerController = null;

    void Awake()
    {
        playerController = transform.parent.GetComponent<PlayerController>();
        myBoxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!myBoxCollider.IsTouchingLayers(-1))
        {
            playerController.SetIsJumping(false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (transform.parent.GetComponent<Rigidbody2D>().velocity.y > 0) {  return; }
        if (!playerController.GetIsJumping())
        {
            playerController.ResetSpeed();
        }
    }
}