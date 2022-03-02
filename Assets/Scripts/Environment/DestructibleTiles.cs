using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace MM.Environment
{
    public class DestructibleTiles : MonoBehaviour
    {
        [SerializeField] TileBase[] floorTiles;
        [SerializeField] float timeToUpdate;

        bool isPlayerTouching = false;
        Tilemap tilemap = null;

        Dictionary<Vector3Int, TileTracker> currentTiles = new Dictionary<Vector3Int, TileTracker>();
        Dictionary<Vector3Int, TileTracker> tileLookup = new Dictionary<Vector3Int, TileTracker>();

        void Awake()
        {
            tilemap = GetComponent<Tilemap>();
        }

        void Update()
        {
            if (currentTiles.Count == 0) { return; }
            if (isPlayerTouching)
            {
                UpdateTouchedTiles();
            }
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.name == "Feet")
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

        private void UpdateTouchedTiles()
        {
            foreach (Vector3Int pos in currentTiles.Keys)
            {
                if (!tileLookup.ContainsKey(pos))
                {
                    tileLookup[pos] = new TileTracker();
                }

                UpdateTile(pos);
            }
        }

        private void UpdateTile(Vector3Int pos)
        {
            TileTracker tracker = tileLookup[pos];

            tracker.SetElapsedTime(tracker.GetElapsedTime() + Time.deltaTime);

            if (tracker.GetElapsedTime() > timeToUpdate)
            {
                UpdateTileTracker(pos, tracker);
            }
        }

        private void UpdateTileTracker(Vector3Int pos, TileTracker tracker)
        {
            int currentIndex = tracker.GetIndex() + 1;
            if (currentIndex < floorTiles.Length)
            {
                ReplaceTile(pos, tracker, currentIndex);
            }
            else
            {
                EraseTile(pos);
            }
        }

        private void ReplaceTile(Vector3Int pos, TileTracker tracker, int currentIndex)
        {
            TileBase newTile = floorTiles[currentIndex];
            tilemap.SetTile(pos, newTile);
            tracker.SetIndex(currentIndex);
            tracker.SetElapsedTime(0);
        }

        private void EraseTile(Vector3Int pos)
        {
            tilemap.SetTile(pos, null);
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
}