using UnityEngine;

public class ExitActivator : MonoBehaviour
{
    BoxCollider2D myBoxCollider = null;
    Collectibles[] collectibles = null;
    int itemsToCollect = 0;

    void Awake()
    {
        myBoxCollider = GetComponent<BoxCollider2D>();
        collectibles = FindObjectsOfType<Collectibles>();
    }

    void Start()
    {
        myBoxCollider.enabled = false;
        itemsToCollect = FindObjectsOfType<Collectibles>().Length;
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
            FindObjectOfType<GameState>().SetPlayGame(false);
            StartCoroutine(FindObjectOfType<Timer>().ConvertAirToScore());
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