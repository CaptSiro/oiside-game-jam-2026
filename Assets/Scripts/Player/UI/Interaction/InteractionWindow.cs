
using System;
using System.Collections;
using System.Linq;
using Environment.Interactable;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Player.UI.Interaction
{
    public class InteractionWindow : UIWindow
    {
        [SerializeField] private CraftingBook craftingBook;
        [SerializeField] private Crafting crafting;
        [SerializeField] private UIController uIController;
        [SerializeField] private Image displayImage;
        [SerializeField] private TextMeshProUGUI entityNameText;

        private EntitySO currentEntity;
        private EntityInteraction currentEntityReference;
        private Sprite[] entitySprites;

        private int EntityAnimationIndex = 0;
        
        public override void ToggleWindow()
        {
            craftingBook.UpdateUI();
            base.ToggleWindow();
        }

        public void SetEntity(EntitySO entity, EntityInteraction entityObject)
        {
            EntityAnimationIndex = 0;
            currentEntity = entity;
            displayImage.sprite = entity.sprites[0];
            entitySprites = entity.sprites;
            entityNameText.text = entity.entityName;
            currentEntityReference = entityObject;
            if (currentEntity.sprites.Length > 1)
            {
                StartCoroutine(AnimateEntity());
            }
        }

        private IEnumerator AnimateEntity()
        {
            while (gameObject.activeSelf)
            {
                displayImage.sprite = entitySprites[EntityAnimationIndex % currentEntity.sprites.Length];
                yield return new WaitForSeconds(0.125f);
                EntityAnimationIndex++;
            }
        }


        public void UseItem(ItemSO usedItem)
        {
            foreach (var t in currentEntity.itemActions)
            {
                if (t.itemSO.itemType == usedItem.itemType)
                {
                    foreach (var ac in t.actions)
                    {
                        HandleAction(ac);
                    }
                    return;
                }
            }

            foreach (var t1 in currentEntity.categoryActions)
            {
                if (usedItem.categories.Any(t => t1.itemCategory == t))
                {
                    foreach (var ac in t1.actions)
                    {
                        HandleAction(ac);
                    }
                    return;
                }
            }
        }

        private void HandleAction(EntitySO.Action actions)
        {
            switch (actions.actionType)
            {
                case EntitySO.ActionType.Destroy:
                    Destroy(currentEntityReference.gameObject);
                    uIController.CloseWindow(1);
                    break;
                
                case EntitySO.ActionType.UnlockNewItem:
                    crafting.UnlockNewItem((ItemSO)actions.resource[0]);
                    break;
                
                case EntitySO.ActionType.ChangeSprite:
                    Sprite[] newSprites = new Sprite[actions.resource.Length];
                    for (int i = 0; i < actions.resource.Length; i++)
                    {
                        newSprites[i] = actions.resource[i] as Sprite;
                    }

                    entitySprites = newSprites;
                    currentEntityReference.UpdateSprites(newSprites);
                    break;
                
                case EntitySO.ActionType.ChangeName:
                    entityNameText.text = actions.text;
                    currentEntityReference.UpdateName(actions.text);
                    break;
                    
            }
        }
    }
}
