using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace System
{
    /// <summary>
    /// Defines where dragged items can be dropped and what happens when they are.
    /// </summary>
    public class ItemDropArea : MonoBehaviour, IDropHandler
    {
        [SerializeField] private UnityEvent<ItemSO> onItemDrop;
        
        
        public void OnDrop(PointerEventData eventData)
        {
            ItemDragSpawner d_i = eventData.pointerDrag.GetComponent<ItemDragSpawner>();
            if (d_i != null)
            {
                onItemDrop?.Invoke(d_i.GetItem());
            }
        }
    }
}
