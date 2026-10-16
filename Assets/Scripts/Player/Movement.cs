using UnityEngine;

namespace Player
{
    public class Movement : MonoBehaviour
    {
        [Header("variables")]
        [SerializeField] private float speed;
        
        [Space(10)]
        [Header("References")]
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private PlayerInput input;

        public void Update()
        {
            rb.linearVelocity = input.moveInput * speed;
        }
    }
}
