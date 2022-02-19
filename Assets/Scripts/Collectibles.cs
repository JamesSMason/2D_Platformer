using System;
using UnityEngine;

public class Collectibles : MonoBehaviour
{
    ExitActivator exitActivator = null;

    public Action OnItemCollected;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) {  return; }
        OnItemCollected();
        Destroy(this.gameObject);
    }
}