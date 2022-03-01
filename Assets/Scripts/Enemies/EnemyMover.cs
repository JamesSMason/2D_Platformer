using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] float speed = 3.0f;
    [SerializeField] bool isHorizontal = true;

    GameState gameState = null;
    Rigidbody2D myRigidbody = null;
    Animator animator = null;

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

        if (isHorizontal)
        {
            float velocity = speed * Mathf.Sign(transform.localScale.x);
            myRigidbody.velocity = new Vector2(velocity, myRigidbody.velocity.y);
        }
        else
        {
            float velocity = speed * Mathf.Abs(transform.localScale.y);
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, velocity);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 enemyScale = transform.localScale;
        enemyScale.x *= -1;
        transform.localScale = enemyScale;
    }
}