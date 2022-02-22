using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DestructibleTiles : MonoBehaviour
{
    [SerializeField] TileBase[] floorSprites;
    [SerializeField] float timeToUpdate;
    bool isPlayerTouching = false;
    Tilemap tilemap = null;

    Dictionary<Vector3Int, TileTracker> currentTiles = new Dictionary<Vector3Int, TileTracker>();
    Dictionary<Vector3Int, TileTracker> tileLookup = new Dictionary<Vector3Int, TileTracker>();
    List<TileBase> tilesToRemove = new List<TileBase>();

    void Awake()
    {
        tilemap = GetComponent<Tilemap>();
    }

    void Update()
    {
        if (currentTiles.Count == 0) { return; }
        if (isPlayerTouching)
        {
            foreach (Vector3Int pos in currentTiles.Keys)
            {
                if (!tileLookup.ContainsKey(pos))
                {
                    tileLookup[pos] = new TileTracker();
                }

                TileTracker tracker = tileLookup[pos];

                float newTime = tracker.GetElapsedTime() + Time.deltaTime;

                if (newTime > timeToUpdate)
                {
                    int currentIndex = tracker.GetIndex() + 1;
                    if (currentIndex < floorSprites.Length)
                    {
                        TileBase newTile = floorSprites[currentIndex];
                        tilemap.SetTile(pos, newTile);
                        tracker.SetIndex(currentIndex);
                        tracker.SetElapsedTime(0);
                    }
                    else
                    {
                        tilemap.SetTile(pos, null);
                    }
                }
                else
                {
                    tracker.SetElapsedTime(newTime);
                }
            }
        }
    }

    void LateUpdate()
    {
        tilemap.RefreshAllTiles();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetType() == typeof(BoxCollider2D))
        {
            isPlayerTouching = true;
            UpdateContacts(collision);
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (isPlayerTouching)
        {
            currentTiles.Clear();
            UpdateContacts(collision);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        currentTiles.Clear();
        isPlayerTouching = false;
    }

    private void UpdateContacts(Collision2D collision)
    {
        foreach (ContactPoint2D hit in collision.contacts)
        {
            Vector3 hitPosition = Vector3.zero;
            hitPosition.x = hit.point.x - 0.1f;
            hitPosition.y = hit.point.y - 0.1f;
            Vector3Int pos = tilemap.WorldToCell(hitPosition);
            TileBase newTile = tilemap.GetTile(pos);
            if (newTile != null)
            {
                currentTiles[pos] = new TileTracker();
            }
        }
    }
}

public class TileTracker
{
    int index = 0;
    float elapsedTime = 0;

    public int GetIndex() { return index; }
    public float GetElapsedTime() { return elapsedTime; }

    public void SetIndex(int index)
    {
        this.index = index;
    }

    public void SetElapsedTime(float value)
    {
        elapsedTime = value;
    }
}