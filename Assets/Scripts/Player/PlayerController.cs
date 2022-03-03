using MM.Core;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MM.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] float velocity = 3.0f;
        [SerializeField] float jumpVelocity = 3.0f;
        [SerializeField] float maxFallSpeed = -10.0f;

        Vector2 moveInput;
        Rigidbody2D myRigidbody = null;
        CollisionDetector myCollider = null;
        GameState gameState = null;

        Animator animator = null;

        bool isOnBelt = false;
        bool firstTouch = false;

        Vector2 beltVelocity = Vector2.zero;

        Vector2 collisionVelocity;

        void Awake()
        {
            myRigidbody = GetComponent<Rigidbody2D>();
            myCollider = GetComponentInChildren<CollisionDetector>();
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
            if (!myCollider.GetIsOnPlatform())
            {
                myRigidbody.velocity = new Vector2(0f, myRigidbody.velocity.y);
                return;
            }
            Run();
            if (myRigidbody.velocity.y == 0)
            {
                if (collisionVelocity.y < maxFallSpeed)
                {
                    collisionVelocity = Vector2.zero;
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
            if (!myCollider.GetIsOnPlatform()) { return; }

            if (value.isPressed)
            {
                myRigidbody.velocity += new Vector2(0f, jumpVelocity);
            }
        }

        public void SetOnConveyor(bool isOnBelt, Vector2 velocity)
        {
            this.isOnBelt = isOnBelt;
            beltVelocity = velocity;
            firstTouch = isOnBelt;
        }

        private void Run()
        {
            if (!myCollider.GetIsOnPlatform()) { return; }

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

            if (myCollider.GetIsOnPlatform())
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
}