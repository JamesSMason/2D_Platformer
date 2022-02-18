using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float velocity = 3.0f;
    [SerializeField] float jumpVelocity = 3.0f;

    Vector2 moveInput;
    Rigidbody2D myRigidbody = null;
    BoxCollider2D myBoxCollider = null;

    void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myBoxCollider = GetComponent<BoxCollider2D>();
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
        if (!GetComponent<BoxCollider2D>().IsTouchingLayers(LayerMask.GetMask("Platform"))) { return; }

        if (value.isPressed)
        {
            myRigidbody.velocity += new Vector2(0f, jumpVelocity);
        }
    }

    private void Run()
    {
        bool touchingPlatform = myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Platform"));
        bool touchingWall = myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Wall"));
        if (!touchingPlatform && !touchingWall) { return; }

        myRigidbody.velocity = new Vector2(moveInput.x * velocity, myRigidbody.velocity.y);
    }
}