using System;
using Environment;
using Environment.Interactable;
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
        
        
        private Interactable interactionObject;
        
        public void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Interactable"))
            {
                interactionKeyBind.SetActive(true);
                interactionObject = other.gameObject.GetComponent<Interactable>();
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
                }
            }
        }
        
        private void NewItemHandling()
        {
            Debug.Log("new item");
            crafting.UnlockNewItem(((ItemPickUp)interactionObject).Item);
            Destroy(interactionObject.gameObject);
        }
    }
    

}
