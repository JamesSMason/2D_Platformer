using UnityEngine;

public class ScenerySwitch : MonoBehaviour
{
    [SerializeField] GameObject switchSprite = null;
    [SerializeField] GameObject scenery = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) { return; }

        switchSprite.GetComponent<SpriteRenderer>().flipX = false;
        scenery.gameObject.SetActive(false);
    }
}