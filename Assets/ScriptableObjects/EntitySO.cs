using System;
using System.Collections;
using Environment.EntityCombat;
using Player;
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
        public class AttackDefinition
        {
            public float attackDelay;
            public ProjectileAttack[] projectileAttacks;
            public ChargeAttack[] chargeAttacks;
        }


        public enum ShootingType
        {
            none,
            directed,
            around,
            circleSequence,
        }

        [Serializable]
        public abstract class AttackBase
        {
            public abstract IEnumerator Execute(EntityCombatScript combatScript);
        }

        [Serializable]
        public class ProjectileAttack : AttackBase
        {
            public ProjectileSO projectile;

            public ShootingType type;

            public int amount;
            public override IEnumerator Execute(EntityCombatScript combatScript)
            {
                yield return combatScript.SpawnProjectileAttack(this);
            }
        }
        
        [Serializable]
        public class ChargeAttack : AttackBase
        {
            public float speed;
            public float damage;
            
            [Space(10)]
            public int numberOfBounces = 0;
            public float maxDistance = -1;
            public bool isGuided;
            public override IEnumerator Execute(EntityCombatScript combatScript)
            {
                yield return combatScript.StartChargeAttack(this);
            }
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
        public float health;
        public float delayBetweenAttacks;
        [SerializeReference] public AttackBase[] attacks;
    }
}
