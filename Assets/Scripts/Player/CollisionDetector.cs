using UnityEngine;

namespace MM.Player
{
    public class CollisionDetector : MonoBehaviour
    {
        BoxCollider2D myBoxCollider = null;

        bool isOnPlatform = false;

        void Awake()
        {
            myBoxCollider = GetComponent<BoxCollider2D>();
        }

        public bool GetIsOnPlatform()
        {
            return isOnPlatform;
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            isOnPlatform = myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Platform"));
        }

        void OnCollisionExit2D(Collision2D collision)
        {
            isOnPlatform = myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Platform"));
        }
    }
}