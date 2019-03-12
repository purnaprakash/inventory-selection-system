using System;
using UnityEngine;

namespace Inventory
{
    public class SlotManager : UnityEngine.MonoBehaviour
    {
        private const string StoredSlotDataKey = "SlotData";
        public SlotDetail headSlot, bodySlot, feetSlot, weaponSlot1, weaponSlot2;

        private SlotData _slotData;

        private void Awake()
        {
            _slotData = new SlotData();
            LoadSlotData();
        }
        
        private void LoadSlotData()
        {
            if (PlayerPrefs.HasKey(StoredSlotDataKey))
            {
                string jsonString = PlayerPrefs.GetString(StoredSlotDataKey);
                _slotData = JsonUtility.FromJson<SlotData>(jsonString);
            }

            headSlot.SetInitialData(_slotData.headSlot);
            bodySlot.SetInitialData(_slotData.bodySlot);
            feetSlot.SetInitialData(_slotData.feetSlot);
            weaponSlot1.SetInitialData(_slotData.weaponSlot1);
            weaponSlot2.SetInitialData(_slotData.weaponSlot2);
        }

        public bool ExistInStoredSlot(string itemName, ItemSlot itemSlot)
        {
            switch (itemSlot)
            {
                case ItemSlot.Weapon:
                    return string.Equals(itemName, weaponSlot1.ItemName) ||
                           string.Equals(itemName, weaponSlot2.ItemName);
                case ItemSlot.Head:
                    return string.Equals(itemName, headSlot.ItemName);
                case ItemSlot.Body:
                    return string.Equals(itemName, bodySlot.ItemName);
                case ItemSlot.Feet:
                    return string.Equals(itemName, feetSlot.ItemName);
            }

            return false;
        }

        public string GetRequestedSlotItemIfExist(ItemSlot itemSlot)
        {
            switch (itemSlot)
            {
                case ItemSlot.Weapon:
                    if (!string.IsNullOrEmpty(weaponSlot1.ItemName) && !string.IsNullOrEmpty(weaponSlot2.ItemName))
                        return weaponSlot1.ItemName;
                    return null;
                case ItemSlot.Head:
                    return string.IsNullOrEmpty(headSlot.ItemName) ? null : headSlot.ItemName;
                case ItemSlot.Body:
                    return string.IsNullOrEmpty(bodySlot.ItemName) ? null : bodySlot.ItemName;
                case ItemSlot.Feet:
                    return string.IsNullOrEmpty(feetSlot.ItemName) ? null : feetSlot.ItemName;
            }

            return null;
        }

        public void AddItemToRespectiveSlot(InventoryItemButton itemButton)
        {
            switch (itemButton.ItemSlot)
            {
                case ItemSlot.Weapon:
                    if (!weaponSlot1.Occupied)
                        weaponSlot1.AddItemIntoSlot(itemButton);
                    else
                        weaponSlot2.AddItemIntoSlot(itemButton);
                    break;
                case ItemSlot.Head:
                    headSlot.AddItemIntoSlot(itemButton);
                    break;
                case ItemSlot.Body:
                    bodySlot.AddItemIntoSlot(itemButton);
                    break;
                case ItemSlot.Feet:
                    feetSlot.AddItemIntoSlot(itemButton);
                    break;
            }
        }

        public void RemoveItemFromSlot(string itemName, ItemSlot itemSlot)
        {
            switch (itemSlot)
            {
                case ItemSlot.Weapon:
                    if (string.Equals(weaponSlot1.ItemName, itemName))
                    {
                        weaponSlot1.RemoveItemFromSlot();
                    }
                    else if (string.Equals(weaponSlot2.ItemName, itemName))
                    {
                        weaponSlot2.RemoveItemFromSlot();
                    }
                    else
                    {
                        Debug.LogErrorFormat("Cannot remove item from slot. Item Name is not valid");
                    }

                    break;
                case ItemSlot.Head:
                    headSlot.RemoveItemFromSlot();
                    break;
                case ItemSlot.Body:
                    bodySlot.RemoveItemFromSlot();
                    break;
                case ItemSlot.Feet:
                    feetSlot.RemoveItemFromSlot();
                    break;
            }
        }

        private void OnApplicationQuit()
        {
            SaveSlotData();
        }

        private void SaveSlotData()
        {
            _slotData.headSlot = headSlot.ItemName;
            _slotData.bodySlot = bodySlot.ItemName;
            _slotData.feetSlot = feetSlot.ItemName;
            _slotData.weaponSlot1 = weaponSlot1.ItemName;
            _slotData.weaponSlot2 = weaponSlot2.ItemName;
            string slotDataString = JsonUtility.ToJson(_slotData, false);
            PlayerPrefs.SetString(StoredSlotDataKey, slotDataString);
        }
    }

    [System.Serializable]
    public struct SlotData
    {
        public string headSlot;
        public string bodySlot;
        public string feetSlot;
        public string weaponSlot1;
        public string weaponSlot2;
    }
}