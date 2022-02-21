using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DestructibleTiles : MonoBehaviour
{
    [SerializeField] TileBase[] floorSprites;
    [SerializeField] float timeToUpdate;
    bool isPlayerTouching = false;
    Tilemap tilemap = null;

    Dictionary<TileBase, Vector3Int> currentTiles = new Dictionary<TileBase, Vector3Int>();
    Dictionary<int, TileTimer> tileLookup = new Dictionary<int, TileTimer>();
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
            foreach (TileBase tile in currentTiles.Keys)
            {
                int tileID = tile.GetInstanceID();

                if (!tileLookup.ContainsKey(tileID))
                {
                    TileTimer tileTimer = new TileTimer();
                    tileLookup[tileID] = tileTimer;
                }

                TileTimer timer = tileLookup[tileID];

                timer.SetElapsedTime(timer.GetElapsedTime() + Time.deltaTime);

                if (timer.GetElapsedTime() > timeToUpdate)
                {
                    int currentIndex = timer.GetIndex();
                    if (currentIndex < floorSprites.Length)
                    {
                        TileBase newTile = floorSprites[currentIndex];
                        tilemap.SetTile(currentTiles[tile], newTile);
                        TileTimer newTimer = new TileTimer();
                        newTimer.SetIndex(currentIndex + 1);
                        tileLookup[newTile.GetInstanceID()] = newTimer;

                        tilesToRemove.Add(tile);
                    }
                    else
                    {
                        tilemap.SetTile(currentTiles[tile], null);
                        tilesToRemove.Add(tile);
                    }
                }
            }

            if (tilesToRemove.Count > 0)
            {
                foreach (TileBase oldTile in tilesToRemove)
                {
                    tileLookup.Remove(oldTile.GetInstanceID());
                }
                tilesToRemove = new List<TileBase>();
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
                currentTiles[newTile] = pos;
            }
        }
    }
}

public class TileTimer
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