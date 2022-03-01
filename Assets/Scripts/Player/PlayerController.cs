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

    Animator animator = null;

    bool isJumping = false;
    bool isOnBelt = false;
    bool firstTouch = false;

    Vector2 beltVelocity = Vector2.zero;

    Vector2 collisionVelocity;

    void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myBoxCollider = GetComponentInChildren<BoxCollider2D>();
        gameState = FindObjectOfType<GameState>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!gameState.GetPlayGame())
        {
            myRigidbody.velocity = Vector2.zero;
            myRigidbody.gravityScale = 0;
            animator.StartPlayback();
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

        if (Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon)
        {
            animator.StopPlayback();
        }
        else
        {
            animator.StartPlayback();
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
        if (!touchingPlatform) { return; }

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
        if (!touchingPlatform) { return; }

        if (isOnBelt)
        {
            if (Mathf.Sign(beltVelocity.x) == Mathf.Sign(moveInput.x) || moveInput.x == 0)
            {
                myRigidbody.velocity = new Vector2(beltVelocity.x, myRigidbody.velocity.y);
                firstTouch = false;
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

        if (!isJumping && touchingPlatform)
        {
            Vector3 playerScale = transform.localScale;
            if (Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon)
            {
                if (Mathf.Sign(playerScale.x) != Mathf.Sign(myRigidbody.velocity.x))
                {
                    playerScale.x *= -1;
                }
                transform.localScale = playerScale;
            }
        }
    }
}