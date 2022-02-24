using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float velocity = 3.0f;
    [SerializeField] float jumpVelocity = 3.0f;

    Vector2 moveInput;
    Rigidbody2D myRigidbody = null;
    BoxCollider2D myBoxCollider = null;

    bool isJumping = false;

    void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        BoxCollider2D[] colliders = GetComponentsInChildren<BoxCollider2D>();
        foreach (BoxCollider2D collider in colliders)
        {
            if (collider.name == "Feet")
            {
                myBoxCollider = collider;
                break;
            }
        }
    }

    void Update()
    {
        Run();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (isJumping) { return; }
        bool touchingPlatform = myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Platform"));
        bool touchingWall = myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Wall"));
        if (!touchingPlatform && !touchingWall) { return; }

        if (value.isPressed)
        {
            SetIsJumping(true);
            myRigidbody.velocity += new Vector2(0f, jumpVelocity);
        }
    }

    public bool GetIsJumping()
    {
        return isJumping;
    }

    public void SetIsJumping(bool isJumping)
    {
        this.isJumping = isJumping;
    }

    public void ResetSpeed()
    {
        myRigidbody.velocity = new Vector2(0f, myRigidbody.velocity.y);
    }

    private void Run()
    {
        bool touchingPlatform = myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Platform"));
        bool touchingWall = myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Wall"));
        if (!touchingPlatform && !touchingWall) { return; }
        myRigidbody.velocity = new Vector2(moveInput.x * velocity, myRigidbody.velocity.y);
    }
}