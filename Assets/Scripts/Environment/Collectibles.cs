using System;
using UnityEngine;

public class Collectibles : MonoBehaviour
{
    [SerializeField] int pointsValue;

    bool hasCollided = false;

    public Action OnItemCollected;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player") || hasCollided) {  return; }
        hasCollided = true;
        OnItemCollected();
        FindObjectOfType<GameState>().IncreaseScore(pointsValue);
        Destroy(this.gameObject);
    }
}