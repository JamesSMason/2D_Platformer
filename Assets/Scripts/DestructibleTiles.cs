using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleTiles : MonoBehaviour
{
    [SerializeField] float destructionTimeInSeconds = 5.0f;

    bool inContactWithPlayer = false;

    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inContactWithPlayer = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inContactWithPlayer = false;
        }
    }
}
