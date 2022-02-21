using System;
using UnityEngine;

public class Collectibles : MonoBehaviour
{
    public Action OnItemCollected;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) {  return; }
        OnItemCollected();
        Destroy(this.gameObject);
    }
}