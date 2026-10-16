using ScriptableObjects;
using TMPro;
using UnityEngine;

namespace Player.UI
{
    public class ItemButton : MonoBehaviour
    {
        [SerializeField] private ItemSO heldItem;
        [SerializeField] private TextMeshProUGUI text;


        public void SetItem(ItemSO item)
        {
            heldItem = item;
            text.text = heldItem.itemType.ToString();
        }
    }
}
