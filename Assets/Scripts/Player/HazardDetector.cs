using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardDetector : MonoBehaviour
{
    bool hitHazard = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Hazard") || hitHazard) { return; }
        hitHazard = true;
        FindObjectOfType<GameState>().LoseLife();
    }
}
