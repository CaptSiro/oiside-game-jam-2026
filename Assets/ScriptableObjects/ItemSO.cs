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
        
        [Header("Properties")]
        public ItemType itemType;
        public bool isInfinite;
        
        [Space(10)]
        [Header("Sources")]
        public ItemType sourceObject1;
        public ItemType sourceObject2;
    }
}
