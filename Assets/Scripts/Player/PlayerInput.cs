using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerInput : MonoBehaviour
    {
        public Vector2 moveInput;
        
        [SerializeField] private Interactions interactions;
        
        
        public void MovementInput(InputAction.CallbackContext context)
        {
            moveInput = context.ReadValue<Vector2>();
        }

        public void Interact(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                interactions.Interact();
            }
        }
    }
}
