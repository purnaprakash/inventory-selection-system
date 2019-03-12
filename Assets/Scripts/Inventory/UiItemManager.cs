using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Inventory
{
    public class UiItemManager : MonoBehaviour
    {
        private enum SortingOrder
        {
            Name,
            Class,
            Type
        }

        public InventoryItemButton uiInventoryItemPrefab;
        public Transform parentForInventoryItems;
        public TMP_Dropdown dropdownForSorting;
        [FormerlySerializedAs("itemColors")] public ItemColor itemBgColors;

        private List<InventoryItemButton> _uiItems = new List<InventoryItemButton>();
        private SortingOrder _currentSortingOrder = SortingOrder.Name;

        private void Awake()
        {
            dropdownForSorting.onValueChanged.AddListener(OnChangedSortingType);
        }

        internal InventoryItemButton SpawnItem()
        {
            InventoryItemButton itemButton = Instantiate(uiInventoryItemPrefab, parentForInventoryItems);
            _uiItems.Add(itemButton);
            return itemButton;
        }

        public Color GetItemBgColor(ItemClass itemClass)
        {
            switch (itemClass)
            {
                case ItemClass.Common:
                    return itemBgColors.colorForCommonClass;
                case ItemClass.Uncommon:
                    return itemBgColors.colorForUncommonClass;
                case ItemClass.Rare:
                    return itemBgColors.colorForRareClass;
                case ItemClass.Legendary:
                    return itemBgColors.colorForLegendaryClass;
                case ItemClass.Mythical:
                default:
                    return itemBgColors.colorForMythicalClass;
            }
        }

        public InventoryItemButton GetButton(string itemName)
        {
            return _uiItems.Find(x => string.Equals(x.ItemName, itemName));
        }

        public void AddIntoInventory(string itemName)
        {
            var itemButton = _uiItems.Find(x => string.Equals(x.ItemName, itemName));
            itemButton.transform.SetParent(parentForInventoryItems);
            itemButton.EnableDrag();
            SortItems();
        }

        #region Sorting

        internal void SortItems()
        {
            switch (_currentSortingOrder)
            {
                case SortingOrder.Name:
                    SortItemsByName();
                    break;
                case SortingOrder.Class:
                    SortItemsByClass();
                    break;
                case SortingOrder.Type:
                    SortItemsByType();
                    break;
            }
        }

        private void SortItemsByName()
        {
            var sortedItemList = _uiItems.OrderBy(x => x.ItemName).ToList();
            _uiItems = sortedItemList;
            UpdateItemOrder();
        }

        private void SortItemsByType()
        {
            var sortedItemList = _uiItems.OrderBy(x => x.ItemSlot.ToString()).ToList();
            _uiItems = sortedItemList;
            UpdateItemOrder();
        }

        private void SortItemsByClass()
        {
            var prioritisedList = _uiItems.Where(x => x.ItemClass == ItemClass.Mythical).ToList();
            var leastPrioritisedList = _uiItems.Where(x => x.ItemClass != ItemClass.Mythical)
                .OrderBy(x => x.ItemClass.ToString()).ToList();
            _uiItems.Clear();
            _uiItems.AddRange(prioritisedList);
            _uiItems.AddRange(leastPrioritisedList);
            UpdateItemOrder();
        }

        private void UpdateItemOrder()
        {
            int length = _uiItems.Count;
            for (int i = 0; i < length; i++)
            {
                _uiItems[i].transform.SetSiblingIndex(i);
            }
        }

        private void OnChangedSortingType(int selectedIndex)
        {
            _currentSortingOrder = (SortingOrder) selectedIndex;
            SortItems();
        }

        #endregion

        private void OnDestroy()
        {
            dropdownForSorting.onValueChanged.RemoveAllListeners();
        }
    }

    [System.Serializable]
    public struct ItemColor
    {
        public Color colorForCommonClass;
        public Color colorForUncommonClass;
        public Color colorForRareClass;
        public Color colorForLegendaryClass;
        public Color colorForMythicalClass;
    }
}