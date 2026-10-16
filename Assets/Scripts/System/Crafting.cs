using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

namespace System
{
    public class Crafting : MonoBehaviour
    {
        private Dictionary<string, ItemSO> craftingResultLookUp;
        
        
        private void Awake()
        {
            craftingResultLookUp = new Dictionary<string, ItemSO>();
            
            //ItemsSO[] items = (ItemSO)UnityEngine.Resources.LoadAll("Items", typeof(ItemSO));
        }
    }
}
