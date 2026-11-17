using ScriptableObjects;
using TMPro;
using UnityEngine;

namespace Player.UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private RectTransform barFill;
        [SerializeField] private TextMeshProUGUI entityName;

        private float maxhealth, currentHealth;

        public void SetUpHealthBar(EntitySO entity)
        {
            float max = entity.health;
            if (max == 0)
            {
                max = 1f;
                return;
            }
            
            gameObject.SetActive(true);
            maxhealth = max;
            currentHealth = max;
            entityName.text = entity.entityName;
            TakeDamage(0);
        }

        public void TakeDamage(float damage)
        {
            currentHealth = Mathf.Max(currentHealth - damage, 0);
            float positionX = -barFill.rect.width + (currentHealth / maxhealth) * barFill.rect.width;
            barFill.pivot = new Vector2(positionX, 0);
        }
    }
}
