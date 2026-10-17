using System.Collections.Generic;
using System.Linq;
using ScriptableObjects;
using UnityEngine;

namespace Player
{
    public class Crafting : MonoBehaviour
    {
        private Dictionary<string, ItemSO> craftingResultLookUp;

        [SerializeField] private List<ItemSO> UnlockedItems;

        public List<ItemSO> GetUnlockedItems()
        {
            return UnlockedItems;
        }

        public ItemSO GetCraftingResult(string key)
        {
            return craftingResultLookUp.GetValueOrDefault(key);
        }
        
        private void Awake()
        {
            craftingResultLookUp = new Dictionary<string, ItemSO>();
            
            Object[] items = UnityEngine.Resources.LoadAll("Items", typeof(ItemSO));

            for (int i = 0; i < items.Length; i++)
            {
                ItemSO current = (ItemSO)items[i];
                bool oneFirst = (int)current.sourceObject1 < (int)current.sourceObject2;
                string key = (oneFirst ? current.sourceObject1.ToString() + current.sourceObject2.ToString() : current.sourceObject2.ToString() + current.sourceObject1.ToString());
                craftingResultLookUp.Add(key, current);
            }
        }

        public void UnlockNewItem(ItemSO item)
        {
            if (UnlockedItems.Any(t => item == t))
            {
                return;
            }

            UnlockedItems.Add(item);
        }
    }
}
