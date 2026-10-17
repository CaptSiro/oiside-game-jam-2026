using System;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Entity", menuName = "Scriptable Objects/EntitySO")]
    public class EntitySO : ScriptableObject
    {
        [Serializable]
        public class ItemActions
        {
            public ItemSO itemSO;
            public UnityEvent onItemAction;
        }

        [Serializable]
        public class CategoryActions
        {
            public ItemSO.Category itemCategory;
            public UnityEvent onCategoryAction;
        }
        
        
        public string entityName;
        public Sprite[] sprites;
        
        public ItemActions[] itemActions;
        public CategoryActions[] categoryActions;
    }
}
