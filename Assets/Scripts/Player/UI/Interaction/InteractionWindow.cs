
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
        [SerializeField] private Image displayImage, playerImage;
        [SerializeField] private TextMeshProUGUI entityNameText, dialogueText, dialogSource;
        [SerializeField] private GameObject KilledScreen, restartButton, leaveButton;
        [SerializeField] private Sprite[] killedPlayerSprites;

        private EntitySO currentEntity;
        private EntityInteraction currentEntityReference;
        private Sprite[] entitySprites;

        private int EntityAnimationIndex = 0;

        private Coroutine writeCoroutine, entityAnimationCoroutine;
        
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

            writeCoroutine = StartCoroutine(WriteDialog(entity.initialLine));

            currentEntity.initialLine = "";
            
            entityNameText.text = entity.entityName;
            currentEntityReference = entityObject;
            if (currentEntity.sprites.Length > 1)
            {
                entityAnimationCoroutine = StartCoroutine(AnimateEntity(displayImage, currentEntity.sprites));
            }
        }

        private IEnumerator AnimateEntity(Image toAnimate, Sprite[] frames)
        {
            while (gameObject.activeSelf)
            {
                toAnimate.sprite = frames[EntityAnimationIndex % frames.Length];
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
                    SoundManager.instance.PlaySound(SoundManager.Sound.correct, transform.position);
                    crafting.UnlockNewItem((ItemSO)actions.resource[0]);
                    dialogSource.text = "";
                    dialogueText.text = "Unlocked new Item: " + ((ItemSO)actions.resource[0]).itemType.ToDisplayString();
                    craftingBook.UpdateUI();
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
                    StopCoroutine(entityAnimationCoroutine);
                    entityAnimationCoroutine = StartCoroutine(AnimateEntity(displayImage, entitySprites));
                    break;
                
                case EntitySO.ActionType.ChangeName:
                    entityNameText.text = actions.text;
                    currentEntityReference.UpdateName(actions.text);
                    break;
                
                case EntitySO.ActionType.TurnOffCollider:
                    currentEntityReference.TurnOffCollider();
                    break;
                
                case EntitySO.ActionType.KillPlayer:
                    SoundManager.instance.PlaySound(SoundManager.Sound.incorrect, transform.position);
                    StartCoroutine(KillPlayerCoroutine(actions.text));
                    break;
                
                case EntitySO.ActionType.SayLine:
                    if (writeCoroutine != null)
                    {
                        StopCoroutine(writeCoroutine);
                    }
                    writeCoroutine = StartCoroutine(WriteDialog(actions.text));
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

        private IEnumerator WriteDialog(string text)
        {
            string[] parts = text.Split('/');
            if (parts.Length > 1)
            {

                for (int i = 0; i < parts.Length; i += 2)
                {
                    if (parts[i].Equals("$"))
                    {
                        dialogSource.text = currentEntity.entityName;
                    }
                    else
                    {
                        dialogSource.text = parts[i];
                    }

                    string line = "";
                    for (int j = 0; j < parts[i + 1].Length; j++)
                    {
                        if (j % 2 == 0)
                        {
                            SoundManager.instance.PlaySound(SoundManager.Sound.speech, transform.position);
                        }
                        line += parts[i + 1][j];
                        dialogueText.text = line;
                        yield return new WaitForSeconds(0.07f);
                    }

                    yield return new WaitForSeconds(3f);
                }
            }
            dialogSource.text = "";
            dialogueText.text = "";
        }

        private IEnumerator KillPlayerCoroutine(string text)
        {
            StartCoroutine(AnimateEntity(playerImage, killedPlayerSprites));
            yield return new WaitForSeconds(3f);
            restartButton.gameObject.SetActive(true);
            leaveButton.gameObject.SetActive(false);
            KilledScreen.SetActive(true);
            dialogueText.text = text;
            craftingBook.gameObject.SetActive(false);
        }
    }
}
