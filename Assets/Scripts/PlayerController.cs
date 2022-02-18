using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float velocity = 3.0f;
    [SerializeField] float jumpVelocity = 3.0f;

    Vector2 moveInput;
    Rigidbody2D myRigidbody = null;

    bool isJumping = false;

    void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
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
        Debug.Log("Hello");
        if (!isJumping)
        {
            myRigidbody.AddForce(Vector2.up * jumpVelocity);
            isJumping = true;
            return;
        }
        isJumping = false;
    }

    private void Run()
    {
        Vector2 playerVelocity = new Vector2 (moveInput.x * velocity, 0);
        myRigidbody.velocity = playerVelocity;
    }
}