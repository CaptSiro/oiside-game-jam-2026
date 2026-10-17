using ScriptableObjects;
using UnityEngine;

namespace Environment.Interactable
{
    public class ItemPickUp : Interactable
    {
        [SerializeField] private ItemSO item;
        [SerializeField] private Sprite afterPickUpSprite;
        [SerializeField] private Collider2D collider2D;
        [SerializeField] private SpriteRenderer spriteRenderer;
        

        public ItemSO PickUp()
        {
            if (afterPickUpSprite != null)
            {
                spriteRenderer.sprite = afterPickUpSprite;
                if (collider2D != null)
                {
                    collider2D.enabled = false;
                }
            }
            else
            {
                Destroy(gameObject);
            }

            return item;
        }
    }
}
