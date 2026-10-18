using System;
using UnityEngine;

namespace ScriptableObjects
{
    
    
    public enum ItemType
    {
        Water,
        Fire,
        Earth,
        Glass,
        Glasses,
        Stone,
        Metal,
        Pipe,
        Wire,
        Steam,
        Plant,
        Rubber,
        Cable,
        Microscope,
        Password,
        Office_Passcode
            
    }

    public static class ItemTypeExtensions
    {
        public static string ToDisplayString(this ItemType type)
        {
            return type.ToString().Replace("_", " ");
        }
    }
    
    [CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
    public class ItemSO : ScriptableObject
    {
        
        public enum Category
        {
            Flammable,
            Extinguisher,
            Solid,
            Weapon
        }
        
        [Header("Properties")]
        public ItemType itemType;

        public bool canBeCrafted;
        public Category[] categories;
        
        [Space(10)]
        [Header("Sources")]
        public ItemType sourceObject1;
        public ItemType sourceObject2;
    }
}
