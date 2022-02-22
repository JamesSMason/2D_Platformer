using System;
using UnityEngine;

public class Collectibles : MonoBehaviour
{
    [SerializeField] int pointsValue;

    GameState gameState;

    public Action OnItemCollected;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) {  return; }
        OnItemCollected();
        FindObjectOfType<GameState>().IncreaseScore(pointsValue);
        Destroy(this.gameObject);
    }
}