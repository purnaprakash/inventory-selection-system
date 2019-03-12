using UnityEngine;
using DictionaryOfItems = System.Collections.Generic.Dictionary<string, Inventory.Item>;

namespace Inventory
{
    public class InventoryManager : MonoBehaviour
    {
        public ItemConfig itemConfig;
        public UiItemManager uiItemManager;
        public SlotManager slotManager;
        public ItemInfoDisplay itemInfoDisplay;
        public OverallStatsDisplay overallStatsDisplay;

        private DictionaryOfItems _itemCollections;

        public static InventoryManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(gameObject);
            else
                Instance = this;
        }

        private void Start()
        {
            int itemsCount = itemConfig.items.Count;
            _itemCollections = new DictionaryOfItems(itemsCount);
            FetchDataAndSpawnItems(itemsCount);
            uiItemManager.SortItems();
            SubscribeEvents();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }

            if (Input.GetKeyDown(KeyCode.F10))
            {
                PlayerPrefs.DeleteAll();
            }
        }

        private void FetchDataAndSpawnItems(int itemsCount)
        {
            /*
         *It's good in performance to do both the job in a loop
         *1. Add record to the dictionary to data fetching 
         *2. Instantiate Inventory Item Button on Ui
        */
            for (int i = 0; i < itemsCount; i++)
            {
                Item item = itemConfig.items[i];
                _itemCollections[item.item_name] = item;
                SpawnItem(item);
            }
        }

        private void SpawnItem(Item item)
        {
            var itemButton = uiItemManager.SpawnItem();
            Color bgColor = uiItemManager.GetItemBgColor(item.item_class);
            itemButton.SetData(item, bgColor);
            itemButton.SubscribeClickEvent(OnClick_ItemButton);
            if (slotManager.ExistInStoredSlot(item.item_name, item.slot))
            {
                itemButton.DisableDrag();
                slotManager.AddItemToRespectiveSlot(itemButton);
                overallStatsDisplay.AddItemAttribute(item);
            }
        }


        private void OnClick_ItemButton(string itemName)
        {
            Item item = _itemCollections[itemName];
            if (slotManager.ExistInStoredSlot(itemName, item.slot))
            {
                itemInfoDisplay.DisplayOccupiedItemInfo(item);
                overallStatsDisplay.UpdateDataOnUi();
                //TODO: Maybe Iff need to show the difference if player unequipped the item
            }
            else
            {
                itemInfoDisplay.DisplaySelectedItemInfo(item);
                string existingItemName = slotManager.GetRequestedSlotItemIfExist(item.slot);
                if (!string.IsNullOrEmpty(existingItemName))
                    overallStatsDisplay.DisplayAttributeDifference(_itemCollections[existingItemName], item);
                else
                    overallStatsDisplay.DisplayAttributeDifference(item);
            }
        }

        private void SubscribeEvents()
        {
            itemInfoDisplay.SubscribeEvents(EquippedItemIntoSlot, UnequippedItemIntoSlot);
        }

        private void UnequippedItemIntoSlot(string itemName)
        {
            Item item = _itemCollections[itemName];
            slotManager.RemoveItemFromSlot(item.item_name, item.slot);
            MoveItemToInventory(item);
        }

        private void EquippedItemIntoSlot(string itemName)
        {
            Item item = _itemCollections[itemName];
            string existingItemName = slotManager.GetRequestedSlotItemIfExist(item.slot);
            if (!string.IsNullOrEmpty(existingItemName))
            {
                Item existingItem = _itemCollections[existingItemName];
                slotManager.RemoveItemFromSlot(existingItemName,existingItem.slot);
                MoveItemToInventory(existingItem);
            }

            MoveItemIntoSlot(item);
        }

        private void MoveItemToInventory(Item item)
        {
            uiItemManager.AddIntoInventory(item.item_name);
            overallStatsDisplay.RemoveItemAttribute(item);
        }

        internal void MoveItemToInventory(string itemName)
        {
            Item item = _itemCollections[itemName];
            uiItemManager.AddIntoInventory(item.item_name);
            overallStatsDisplay.RemoveItemAttribute(item);
        }

        private void MoveItemIntoSlot(Item item)
        {
            InventoryItemButton itemButton = uiItemManager.GetButton(item.item_name);
            itemButton.DisableDrag();
            slotManager.AddItemToRespectiveSlot(itemButton);
            overallStatsDisplay.AddItemAttribute(item);
        }

        internal void AddAttributeIntoOverallStats(string itemName)
        {
            overallStatsDisplay.AddItemAttribute(_itemCollections[itemName]);
        }
    }
}