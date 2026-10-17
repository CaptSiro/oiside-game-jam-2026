using System;
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
            KillPlayer,
            
        }

        [Serializable]
        public class Action
        {
            public ActionType actionType;
            public Object[] resource;
            public int parameter;
            public string text;
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
        
        public ItemActions[] itemActions;
        public CategoryActions[] categoryActions;
    }
}
