using System;
using Player.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player
{
    public class PlayerHealthBar : PlayerBaseClass
    {
        [SerializeField] private float MaxHealth = 100f;
            
        private float Health;
        private UIController uiController;

        private void Awake()
        {
            uiController = playerRef.GetScript<UIController>();
        }

        public void TakeDamage(float damage)
        {
            Health -= damage;
            uiController.UpdateHealthBar(damage, true);
            
            if (Health <= 0)
            {
                //TODO death sequence
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        public void ResetHealth()
        {
            Health = MaxHealth;
            uiController.SetUpPlayerHealthBar(MaxHealth);
        }
        
    }
}
