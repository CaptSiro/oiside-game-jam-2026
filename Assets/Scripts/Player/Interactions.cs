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
            if (other.gameObject.layer == LayerMask.NameToLayer("Interactable"))
            {
                interactionObject = other.gameObject.GetComponent<Interactable>();
                if (hasBook || interactionObject.InteractableType == Interactable.InteractableTypeEnum.BookPickUp)
                {
                    interactionKeyBind.SetActive(true);
                }
                else
                {
                    interactionObject = null;
                }
                
            }
        }

        public void OnCollisionExit2D(Collision2D other)
        {
            if (interactionObject != null && other.gameObject == interactionObject.gameObject)
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
    }
    

}
