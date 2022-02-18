using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] float speed = 3.0f;
    [SerializeField] bool isHorizontal = true;

    Rigidbody2D myRigidbody = null;
    CapsuleCollider2D myCapsuleCollider = null;

    void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
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
        Debug.Log("Colliding");
        if (!myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Wall"))) { return; }
        Debug.Log("Success");

        Vector3 enemyScale = transform.localScale;
        enemyScale.x *= -1;
        transform.localScale = enemyScale;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        Vector3 enemyScale = transform.localScale;
        enemyScale.x *= -1;
        transform.localScale = enemyScale;
    }
}
