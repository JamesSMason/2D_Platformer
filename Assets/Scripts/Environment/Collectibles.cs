using MM.Core;
using System;
using UnityEngine;

namespace MM.Environment
{
    public class Collectibles : MonoBehaviour
    {
        [SerializeField] int pointsValue;

        GameState gameState = null;
        Animator animator = null;

        bool hasCollided = false;

        public Action OnItemCollected;

        void Awake()
        {
            gameState = FindObjectOfType<GameState>();
            animator = GetComponent<Animator>();
        }

        void Update()
        {
            if (!gameState.GetPlayGame())
            {
                animator.StartPlayback();
            }
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player") || hasCollided) { return; }
            hasCollided = true;
            OnItemCollected();
            FindObjectOfType<GameState>().IncreaseScore(pointsValue);
            Destroy(this.gameObject);
        }
    }
}