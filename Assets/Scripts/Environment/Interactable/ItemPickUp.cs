using ScriptableObjects;
using UnityEngine;

namespace Environment.Interactable
{
    public class ItemPickUp : Interactable
    {
        [SerializeField] private ItemSO item;
        
        public ItemSO Item => item;
    }
}
