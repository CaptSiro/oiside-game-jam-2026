using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

namespace Environment.Interactable
{
    public class EntityInteraction : Interactable
    {
        [SerializeField] private EntitySO entity;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Collider2D collider;
        
        public EntitySO Entity => entity;
        private int EntityAnimationIndex = 0;

        private void Awake()
        {
            entity = Instantiate(entity);
            if (interactableType != InteractableTypeEnum.Clue)
            {
                spriteRenderer.sprite = entity.sprites[0];
                if (entity.sprites.Length > 1)
                {
                    StartCoroutine(AnimateEntity());
                }
            }

        }

        private IEnumerator AnimateEntity()
        {
            while (gameObject.activeSelf)
            {
                spriteRenderer.sprite = entity.sprites[EntityAnimationIndex % entity.sprites.Length];
                yield return new WaitForSeconds(0.125f);
                EntityAnimationIndex++;
            }
        }

        public void UpdateSprites(Sprite[] sprites)
        {
            entity.sprites = sprites;
            spriteRenderer.sprite = entity.sprites[0];
        }

        public void UpdateName(string newName)
        {
            entity.name = newName;
        }

        public void TurnOffCollider()
        {
            collider.enabled = false;
        }
    }
}
