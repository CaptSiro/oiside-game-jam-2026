
using System;
using System.Collections;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Player.UI.Interaction
{
    public class InteractionWindow : UIWindow
    {
        [SerializeField] private CraftingBook craftingBook;
        [SerializeField] private Image displayImage;
        [SerializeField] private TextMeshProUGUI entityNameText;

        private EntitySO currentEntity;

        private int EntityAnimationIndex = 0;
        
        public override void ToggleWindow()
        {
            craftingBook.UpdateUI();
            base.ToggleWindow();
        }

        public void SetEntity(EntitySO entity)
        {
            EntityAnimationIndex = 0;
            currentEntity = entity;
            displayImage.sprite = entity.sprites[0];
            entityNameText.text = entity.entityName;
            if (currentEntity.sprites.Length > 1)
            {
                StartCoroutine(AnimateEntity());
            }
        }

        private IEnumerator AnimateEntity()
        {
            while (gameObject.activeSelf)
            {
                displayImage.sprite = currentEntity.sprites[EntityAnimationIndex % currentEntity.sprites.Length];
                yield return new WaitForSeconds(0.125f);
                EntityAnimationIndex++;
            }
        }
    }
}
