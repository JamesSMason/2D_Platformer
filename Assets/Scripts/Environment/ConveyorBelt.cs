using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    [SerializeField] float beltSpeed = 3.0f;

    Vector2 velocity = Vector2.zero;

    void Awake()
    {
        velocity = new Vector2(beltSpeed, 0.0f);
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