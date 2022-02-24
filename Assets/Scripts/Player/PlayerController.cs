using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float velocity = 3.0f;
    [SerializeField] float jumpVelocity = 3.0f;
    [SerializeField] float maxFallSpeed = -10.0f;

    Vector2 moveInput;
    Rigidbody2D myRigidbody = null;
    BoxCollider2D myBoxCollider = null;
    GameState gameState = null;

    bool isJumping = false;
    bool isOnBelt = false;
    bool firstTouch = false;

    Vector2 beltVelocity = Vector2.zero;

    Vector2 collisionVelocity;

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
        gameState = FindObjectOfType<GameState>();
    }

    void Update()
    {
        if (!gameState.GetPlayGame())
        {
            myRigidbody.velocity = Vector2.zero;
            myRigidbody.gravityScale = 0;
            return;
        }
        myRigidbody.gravityScale = 1;
        Run();
        if (myRigidbody.velocity.y == 0)
        {
            if (collisionVelocity.y < maxFallSpeed)
            {
                FindObjectOfType<GameState>().LoseLife();
            }
        }
    }

    void FixedUpdate()
    {
        collisionVelocity = myRigidbody.velocity;
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        if (moveInput.x == 0)
        {
            firstTouch = false;
        }
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

    public void SetOnConveyor(bool isOnBelt, Vector2 velocity)
    {
        this.isOnBelt = isOnBelt;
        beltVelocity = velocity;
        firstTouch = isOnBelt;
    }

    private void Run()
    {
        bool touchingPlatform = myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Platform"));
        bool touchingWall = myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Wall"));
        if (!touchingPlatform && !touchingWall) { return; }

        if (isOnBelt)
        {
            if (Mathf.Sign(beltVelocity.x) == Mathf.Sign(moveInput.x))
            {
                myRigidbody.velocity = new Vector2(beltVelocity.x, myRigidbody.velocity.y);
            }
            else
            {
                if (firstTouch)
                {
                    myRigidbody.velocity = new Vector2(moveInput.x * velocity, myRigidbody.velocity.y);
                }
                else
                {
                    myRigidbody.velocity = new Vector2(beltVelocity.x + (moveInput.x * velocity), myRigidbody.velocity.y);
                }
            }
        }
        else
        {
            myRigidbody.velocity = new Vector2(moveInput.x * velocity, myRigidbody.velocity.y);
        }
    }
}