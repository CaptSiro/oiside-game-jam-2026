using System;
using Environment;
using Environment.Interactable;
using Player.UI;
using Player.UI.Interaction;
using Unity.VisualScripting;
using UnityEngine;

namespace Player
{
    public class Interactions : MonoBehaviour
    {
        [SerializeField] private GameObject interactionKeyBind;
        
        [Space(10)]
        [Header("References")]
        [SerializeField] private Crafting crafting;
        [SerializeField] private UIController uiController;
        
        
        private Interactable interactionObject;
        private bool hasBook = false;
        
        public void OnCollisionEnter2D(Collision2D other)
        {
            AddInteractable(other.gameObject);
        }

        public void OnCollisionExit2D(Collision2D other)
        {
            RemoveInteractable(other.gameObject);
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            AddInteractable(other.gameObject);
        }

        public void OnTriggerExit2D(Collider2D other)
        {
            RemoveInteractable(other.gameObject);
        }

        private void AddInteractable(GameObject interactable)
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

        private void RemoveInteractable(GameObject interactable)
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
        }
    }
    

}
