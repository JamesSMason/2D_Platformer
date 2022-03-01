using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    BoxCollider2D myBoxCollider = null;
    PlayerController playerController = null;

    bool isFalling = false;

    void Awake()
    {
        playerController = transform.parent.GetComponent<PlayerController>();
        myBoxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (isFalling)
        {
            playerController.ResetSpeed();
            isFalling = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        bool isOnPlatform = myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Platform"));
        bool isNotOnGround = !isOnPlatform;
        if (!isNotOnGround)
        {
            playerController.SetIsJumping(false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (transform.parent.GetComponent<Rigidbody2D>().velocity.y > Mathf.Epsilon) { return; }
        if (!playerController.GetIsJumping())
        {
            isFalling = true;
        }
    }
}