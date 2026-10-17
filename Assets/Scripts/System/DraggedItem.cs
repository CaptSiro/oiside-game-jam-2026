using ScriptableObjects;
using TMPro;
using UnityEngine;

namespace System
{
    public class DraggedItem : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;
        

        public void SetItem(ItemSO item)
        {
            text.text = item.itemType.ToString();
        }

        public void UpdatePosition(Vector3 update)
        {
            ((RectTransform)transform).localPosition = update;
        }
    }
}
