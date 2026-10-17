using System;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
    public class ItemSO : ScriptableObject
    {

        public enum ItemType
        {
            Water,
            Fire,
            Earth
        }

        public enum Category
        {
            Flammable,
        }
        
        [Header("Properties")]
        public ItemType itemType;
        public bool isInfinite;
        public Category[] categories;
        
        [Space(10)]
        [Header("Sources")]
        public ItemType sourceObject1;
        public ItemType sourceObject2;
    }
}
