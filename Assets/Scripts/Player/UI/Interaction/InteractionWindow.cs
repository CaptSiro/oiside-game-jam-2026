
using System;
using System.Collections;
using System.Linq;
using Environment.Interactable;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Player.UI.Interaction
{
    public class InteractionWindow : UIWindow
    {
        [SerializeField] private CraftingBook craftingBook;
        [SerializeField] private Crafting crafting;
        [SerializeField] private UIController uIController;
        [SerializeField] private Image displayImage;
        [SerializeField] private TextMeshProUGUI entityNameText, dialogueText;
        [SerializeField] private GameObject KilledScreen, restartButton, leaveButton;

        private EntitySO currentEntity;
        private EntityInteraction currentEntityReference;
        private Sprite[] entitySprites;

        private int EntityAnimationIndex = 0;
        
        public override void ToggleWindow()
        {
            craftingBook.gameObject.SetActive(true);
            craftingBook.UpdateUI();
            base.ToggleWindow();
        }

        public void SetToClue()
        {
            craftingBook.gameObject.SetActive(false);
        }

        public void SetEntity(EntitySO entity, EntityInteraction entityObject)
        {
            EntityAnimationIndex = 0;
            currentEntity = entity;
            displayImage.sprite = entity.sprites[0];
            entitySprites = entity.sprites;
            dialogueText.text = "";
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
                    entityNameText.text = "";
                    StartCoroutine(DelayClose());
                    break;
                
                case EntitySO.ActionType.UnlockNewItem:
                    crafting.UnlockNewItem((ItemSO)actions.resource[0]);
                    dialogueText.text = "Unlocked new Item: " + ((ItemSO)actions.resource[0]).itemType.ToDisplayString();
                    //craftingBook.UpdateUI();
                    break;
                
                case EntitySO.ActionType.ChangeSprite:
                    Sprite[] newSprites = new Sprite[actions.resource.Length];
                    for (int i = 0; i < actions.resource.Length; i++)
                    {
                        newSprites[i] = actions.resource[i] as Sprite;
                    }

                    entitySprites = newSprites;
                    currentEntityReference.UpdateSprites(newSprites);
                    displayImage.sprite = entitySprites[0];
                    break;
                
                case EntitySO.ActionType.ChangeName:
                    entityNameText.text = actions.text;
                    currentEntityReference.UpdateName(actions.text);
                    break;
                
                case EntitySO.ActionType.TurnOffCollider:
                    currentEntityReference.TurnOffCollider();
                    break;
                
                case EntitySO.ActionType.KillPlayer:
                    restartButton.gameObject.SetActive(true);
                    leaveButton.gameObject.SetActive(false);
                    KilledScreen.SetActive(true);
                    dialogueText.text = actions.text;
                    craftingBook.gameObject.SetActive(false);
                    break;
                    
            }
        }

        private IEnumerator DelayClose()
        {
            yield return null;
            uIController.CloseWindow(1);
        }

        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
