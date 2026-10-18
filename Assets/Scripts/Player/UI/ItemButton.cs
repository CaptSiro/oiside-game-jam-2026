using System;
using ScriptableObjects;
using TMPro;
using UnityEngine;

namespace Player.UI
{
    public class ItemButton : MonoBehaviour
    {
        [SerializeField] private ItemSO heldItem;
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private ItemDragSpawner dragSpawner;

        
        public void SetItem(ItemSO item)
        {
            heldItem = item;
            text.text = heldItem.itemType.ToDisplayString();
            dragSpawner.SetItem(item);
        }
    }
}
