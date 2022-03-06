using MM.Core;
using UnityEngine;

namespace MM.Enemies
{
    public class EnemyMover : MonoBehaviour
    {
        [SerializeField] float speed = 3.0f;
        [SerializeField] float fallSpeed = -3.0f;
        [SerializeField] bool isHorizontal = true;

        GameState gameState = null;
        Rigidbody2D myRigidbody = null;
        Animator animator = null;

        bool isFalling = false;

        void Awake()
        {
            myRigidbody = GetComponent<Rigidbody2D>();
            gameState = FindObjectOfType<GameState>();
            animator = GetComponent<Animator>();
        }

        void Update()
        {
            if (!gameState.GetPlayGame())
            {
                myRigidbody.velocity = Vector3.zero;
                animator.StartPlayback();
                return;
            }

            if (isFalling) { return; }

            if (isHorizontal)
            {
                float velocity = speed * Mathf.Sign(transform.localScale.x);
                myRigidbody.velocity = new Vector2(velocity, myRigidbody.velocity.y);
            }
            else
            {
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, speed);
            }
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (isHorizontal)
            {
                Vector3 enemyScale = transform.localScale;
                enemyScale.x *= -1;
                transform.localScale = enemyScale;
            }
            else
            {
                speed *= -1;
            }
        }

        public void SetIsFalling()
        {
            isFalling = true;
            GetComponent<CapsuleCollider2D>().isTrigger = true;
            myRigidbody.velocity = new Vector2(0f, fallSpeed);
        }
    }
}