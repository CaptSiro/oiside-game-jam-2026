using System;
using Player.UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerInput : PlayerBaseClass
    {
        public Vector2 moveInput;
        
        private Interactions interactions;
        private UIController uiController;


        private void Awake()
        {
            interactions = playerRef.GetComponent<Interactions>();
            uiController = playerRef.GetComponent<UIController>();
        }
        

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

        public void PauseInput(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                uiController.TogglePause();
            }
        }
    }
}
