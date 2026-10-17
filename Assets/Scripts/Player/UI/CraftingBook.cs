using System;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Player.UI
{
    public class CraftingBook : UIWindow
    {
        [SerializeField] private Crafting crafting;
        [SerializeField] private ItemButton itemButtonPrefab;
        [SerializeField] private Transform itemHolder;
        [SerializeField] private int buttonsPerPage;
        [SerializeField] private GameObject nextButton, prevButton;
        
        
        private int lastShowAmount = 0;
        private List<ItemButton> itemButtons = new List<ItemButton>();

        private int page;
        
        public void UpdateUI()
        {
            List<ItemSO> UnlockedItems = crafting.GetUnlockedItems();
            for (int i = lastShowAmount; i < UnlockedItems.Count; i++)
            {
                ItemButton itemButton = Instantiate(itemButtonPrefab, itemHolder);
                itemButton.SetItem(UnlockedItems[i]);
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
        
        
        
    }
}
