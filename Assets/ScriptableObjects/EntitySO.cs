using System;
using ScriptableObjects.EntityProjectiles;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Entity", menuName = "Scriptable Objects/EntitySO")]
    public class EntitySO : ScriptableObject
    {
        public enum ActionType
        {
            Destroy,
            ChangeSprite,
            UnlockNewItem,
            ChangeName,
            TurnOffCollider,
            StartCombat,
            SayLine,
        }

        public enum CombatType
        {
            None,
            BulletHell,
            Charging
        }

        /// <summary>
        /// Defines interactions of entity with items. Uses Action type enum as what should happen and has fields for possible resources the action needs. Some action need none some need a picture and so on.
         /// </summary>
        [Serializable]
        public class Action
        {
            public ActionType actionType;
            public Object[] resource;
            public int parameter;
            public string text;
        }

        [Serializable]
        public class BulletHellDefinition
        {
            public ProjectileAttack[] projectileAttacks;
            public float attackDelay;
        }
        
        public enum ShootingType
        {
            none,
            directed,
            around,
            circleSequence,
        }

        [Serializable]
        public class ProjectileAttack
        {
            public ProjectileSO bullet;

            public ShootingType type;

            public int amount;
        }
        
        [Serializable]
        public class ItemActions
        {
            public ItemSO itemSO;
            public Action[] actions;
        }

        [Serializable]
        public class CategoryActions
        {
            public ItemSO.Category itemCategory;
            public Action[] actions;
        }
        
        
        public string entityName;
        public Sprite[] sprites;
        public string initialLine;  //What is written at the bottom of the screen when interaction begins (Same format as dialog)
        
        [Space(10)]
        [Header("Actions")]
        public ItemActions[] itemActions;
        public CategoryActions[] categoryActions;

        [Space(10)]
        [Header("Combat")]
        public CombatType combatType;
        public BulletHellDefinition bulletHellDefinition;
    }
}
