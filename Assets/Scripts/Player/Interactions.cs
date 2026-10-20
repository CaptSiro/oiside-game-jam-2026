using System;
using Environment;
using Environment.Interactable;
using Player.UI;
using Player.UI.Interaction;
using Unity.VisualScripting;
using UnityEngine;

namespace Player
{
    /// <summary>
    /// Detects interactable object via colliders and triggers. After object is activated handles the interaction based on object type.
    /// </summary>
    public class Interactions : PlayerBaseClass
    {
        [SerializeField] private GameObject interactionKeyBind;
        
        private Crafting crafting;
        private UIController uiController;
        private Interactable interactionObject;
        private bool hasBook = false;

        private void Awake()
        {
            crafting = playerRef.GetComponent<Crafting>();
            uiController = playerRef.GetScript<UIController>();
        }

        public void OnCollisionEnter2D(Collision2D other)
        {
            NewCollision(other.gameObject);
        }

        public void OnCollisionExit2D(Collision2D other)
        {
            CollisionEnded(other.gameObject);
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            NewCollision(other.gameObject);
        }

        public void OnTriggerExit2D(Collider2D other)
        {
            CollisionEnded(other.gameObject);
        }

        private void NewCollision(GameObject interactable)
        {
            if (interactable.layer == LayerMask.NameToLayer("Interactable"))
            {
                interactionObject = interactable.gameObject.GetComponent<Interactable>();
                if (hasBook || interactionObject.InteractableType == Interactable.InteractableTypeEnum.BookPickUp)
                {
                    interactionKeyBind.SetActive(true);
                }
                else
                {
                    interactionObject = null;
                }
                
            }

            if (interactable.layer == LayerMask.NameToLayer("End"))
            {
                uiController.ShowEndScreen();
            }
        }

        private void CollisionEnded(GameObject interactable)
        {
            if (interactionObject != null && interactable == interactionObject.gameObject)
            {
                interactionKeyBind.SetActive(false);
                interactionObject = null;
            }
        }

        public void Interact()
        {
            if (interactionObject != null)
            {
                switch (interactionObject.InteractableType)
                {
                    case Interactable.InteractableTypeEnum.NewItem:
                        NewItemHandling();
                        break;
                    
                    case Interactable.InteractableTypeEnum.Puzzle:
                        EntityInteract();
                        break;
                    
                    case Interactable.InteractableTypeEnum.BookPickUp:
                        hasBook = true;
                        uiController.ShowBook();
                        Destroy(interactionObject.gameObject);
                        break;
                    
                    case Interactable.InteractableTypeEnum.NPC:
                        EntityInteract();
                        break;
                    
                    case Interactable.InteractableTypeEnum.Clue:
                        ClueInteract();
                        break;
                }
            }
        }
        
        private void NewItemHandling()
        {
            crafting.UnlockNewItem(((ItemPickUp)interactionObject).PickUp());
            
        }

        private void EntityInteract()
        {
            InteractionWindow intWin = (InteractionWindow)uiController.OpenWindow(1);
            intWin.SetEntity(((EntityInteraction)interactionObject).Entity, (EntityInteraction)interactionObject);
        }

        private void ClueInteract()
        {
            InteractionWindow intWin = (InteractionWindow)uiController.OpenWindow(1);
            intWin.SetEntity(((EntityInteraction)interactionObject).Entity, (EntityInteraction)interactionObject);
            intWin.SetToClue();
            Destroy(interactionObject.gameObject);
        }
    }
    

}
