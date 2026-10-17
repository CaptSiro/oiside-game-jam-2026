using ScriptableObjects;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;


namespace System
{
    public class ItemDragSpawner : MonoBehaviour,  IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private DraggedItem itemPrefab;
        [SerializeField] private RectTransform dragLayer;

        private DraggedItem draggedItem;
        private Canvas canvas;
        private ItemSO item;

        public ItemSO GetItem()
        {
            return item;
        }
        
        public void SetItem(ItemSO itemSO)
        {
            item = itemSO;
        }
        
        public void SetDragLayer(RectTransform rectTransform)
        {
            dragLayer = rectTransform;
        }

        private void Awake()
        {
            canvas = GetComponentInParent<Canvas>().rootCanvas;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            draggedItem = Instantiate(itemPrefab, dragLayer);
            
            draggedItem.SetItem(item);

            draggedItem.GetComponent<CanvasGroup>().blocksRaycasts = false;

            UpdatePosition(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (draggedItem != null)
                UpdatePosition(eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (draggedItem == null)
                return;
            
            Destroy(draggedItem.gameObject);

            draggedItem = null;
        }

        private void UpdatePosition(PointerEventData eventData)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                dragLayer,
                eventData.position,
                canvas.renderMode == RenderMode.ScreenSpaceOverlay
                    ? null
                    : canvas.worldCamera,
                out Vector2 localPoint
            );

            draggedItem.UpdatePosition(localPoint);
        }
    }
}
