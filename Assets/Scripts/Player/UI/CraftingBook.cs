using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Player.UI
{
    public class CraftingBook : UIWindow
    {
        [SerializeField] private Crafting crafting;
        [SerializeField] private RectTransform dragLayer;
        [SerializeField] private ItemButton itemButtonPrefab;
        [SerializeField] private Transform itemHolder;
        [SerializeField] private int buttonsPerPage;
        [SerializeField] private GameObject nextButton, prevButton;
        [SerializeField] private TextMeshProUGUI[] craftingSlotsText;

        private ItemSO[] slotItems = new ItemSO[2];
        
        
        private int lastShowAmount = 0;
        private List<ItemButton> itemButtons = new List<ItemButton>();

        private int page;
        
        public void UpdateUI()
        {
            slotItems = new ItemSO[2];
            foreach (var cst in craftingSlotsText)
            {
                cst.text = "";
            }
            
            List<ItemSO> UnlockedItems = crafting.GetUnlockedItems();
            for (int i = lastShowAmount; i < UnlockedItems.Count; i++)
            {
                ItemButton itemButton = Instantiate(itemButtonPrefab, itemHolder);
                itemButton.SetItem(UnlockedItems[i]);
                itemButton.GetComponent<ItemDragSpawner>().SetDragLayer(dragLayer);
                itemButtons.Add(itemButton);
            }
            lastShowAmount = UnlockedItems.Count;
            SelectPage(0);
        }

        private void SelectPage(int toShow)
        {
            for (int i = 0; i < itemButtons.Count; i++)
            {
                itemButtons[i].gameObject.SetActive(i >= toShow * buttonsPerPage && i < (toShow+1) * buttonsPerPage);
            }
            
            page = toShow;
            nextButton.SetActive(toShow < (itemButtons.Count / buttonsPerPage));
            prevButton.SetActive(toShow > 0);
        }

        public void NextPage()
        {
            SelectPage(page + 1);
        }

        public void PreviousPage()
        {
            SelectPage(page - 1);
        }

        public void SetCratingSlot0(ItemSO item)
        {
            SetCraftingSlot(item, 0);
        }

        public void SetCraftingSlot1(ItemSO item)
        {
            SetCraftingSlot(item, 1);
        }
        
        public void SetCraftingSlot(ItemSO item, int slot)
        {
            slotItems[slot] = item;
            craftingSlotsText[slot].text = item.itemType.ToString();

            if (slotItems[0] != null && slotItems[1] != null)
            {
                bool oneFirst = slotItems[0].itemType < slotItems[1].itemType;
                string key = (oneFirst ? slotItems[0].itemType.ToString() + slotItems[1].itemType.ToString() : slotItems[1].itemType.ToString() + slotItems[0].itemType.ToString());
                ItemSO unlockedItem = crafting.GetCraftingResult(key);
                if (unlockedItem != null)
                {
                    craftingSlotsText[2].text = unlockedItem.itemType.ToString();
                    crafting.UnlockNewItem(unlockedItem);
                    StartCoroutine(DelayNewItem());
                }
                
            }
        }

        private IEnumerator DelayNewItem()
        {
            yield return new WaitForSeconds(0.5f);
            UpdateUI();
        }
        
    }
}
