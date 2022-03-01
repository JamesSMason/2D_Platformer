using UnityEngine;
using UnityEngine.Tilemaps;

public class ConveyorBelt : MonoBehaviour
{
    [SerializeField] float beltSpeed = 3.0f;

    Vector2 velocity = Vector2.zero;

    GameState gameState = null;
    Tilemap tilemap = null;

    void Awake()
    {
        gameState = FindObjectOfType<GameState>();
        tilemap = GetComponent<Tilemap>();
        velocity = new Vector2(beltSpeed, 0.0f);
    }

    void Update()
    {
        if (!gameState.GetPlayGame())
        {
            tilemap.animationFrameRate = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == "Feet")
        {
            FindObjectOfType<PlayerController>().SetOnConveyor(true, velocity);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.name == "Feet")
        {
            FindObjectOfType<PlayerController>().SetOnConveyor(false, Vector2.zero);
        }
    }
}