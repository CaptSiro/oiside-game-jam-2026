using UnityEngine;

namespace Player
{
    public class Movement : MonoBehaviour
    {
        private static readonly int WalkingUp = Animator.StringToHash("walkingUp");
        private static readonly int Speed = Animator.StringToHash("Speed");

        [Header("variables")]
        [SerializeField] private float speed;
        
        [Space(10)]
        [Header("References")]
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private PlayerInput input;
        [SerializeField] private Animator animator;

        public void Update()
        {
            rb.linearVelocity = input.moveInput * speed;
            if (input.moveInput == Vector2.zero)
            {
                animator.SetFloat("Speed", 0);
            }
            else
            {
                animator.SetFloat(Speed, 1);
                animator.SetBool(WalkingUp, input.moveInput.y > 0);
            }
        }
    }
}
