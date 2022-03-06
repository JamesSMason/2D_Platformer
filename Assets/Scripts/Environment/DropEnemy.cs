using MM.Enemies;
using UnityEngine;

public class DropEnemy : MonoBehaviour
{
    [SerializeField] GameObject switchSprite = null;
    [SerializeField] EnemyMover enemy = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) { return; }

        switchSprite.GetComponent<SpriteRenderer>().flipX = false;
        enemy.SetIsFalling();
        enemy.GetComponent<Animator>().SetTrigger("isFalling");
    }
}