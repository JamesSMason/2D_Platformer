using MM.Core;
using UnityEngine;

namespace MM.Environment
{
    public class ExitActivator : MonoBehaviour
    {
        BoxCollider2D myBoxCollider = null;
        Collectibles[] collectibles = null;
        GameState gameState = null;

        int itemsToCollect = 0;

        void Awake()
        {
            myBoxCollider = GetComponent<BoxCollider2D>();
            collectibles = FindObjectsOfType<Collectibles>();
            gameState = FindObjectOfType<GameState>();
        }

        void Start()
        {
            myBoxCollider.enabled = false;
            itemsToCollect = collectibles.Length;
        }

        void OnEnable()
        {
            if (collectibles == null) { return; }
            foreach (Collectibles collectible in collectibles)
            {
                collectible.OnItemCollected += ReduceCollectibles;
            }
        }

        void OnDisable()
        {
            if (collectibles == null) { return; }
            foreach (Collectibles collectible in collectibles)
            {
                collectible.OnItemCollected -= ReduceCollectibles;
            }
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                gameState.LevelComplete();
            }
        }

        private void ReduceCollectibles()
        {
            itemsToCollect--;
            if (itemsToCollect <= 0)
            {
                myBoxCollider.enabled = true;
            }
        }
    }
}