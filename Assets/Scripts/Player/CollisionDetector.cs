using UnityEngine;

namespace MM.Player
{
    public class CollisionDetector : MonoBehaviour
    {
        BoxCollider2D myBoxCollider = null;

        bool isOnPlatform = false;
        bool isJumping = false;

        void Awake()
        {
            myBoxCollider = GetComponent<BoxCollider2D>();
        }

        public bool GetIsOnPlatform()
        {
            return isOnPlatform;
        }

        public bool GetIsJumping()
        {
            return isJumping;
        }

        public void SetIsJumping(bool isJumping)
        {
            this.isJumping = isJumping;
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Platform")))
            {
                isOnPlatform = true;
                isJumping = false;
            }
        }

        void OnCollisionExit2D(Collision2D collision)
        {
            if (!myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Platform")))
            {
                isOnPlatform = false;
            }
        }
    }
}