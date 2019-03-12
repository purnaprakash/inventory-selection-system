using TMPro;
using UnityEngine;

namespace Inventory
{
    public class SlotDetail : MonoBehaviour
    {
        public ItemSlot slotType;
        public TextMeshProUGUI textSlotLabel;
        public bool Occupied { get; private set; } = false;
        public string ItemName { get; private set; } = null;

        private RectTransform _rectTransform;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        internal void SetInitialData(string itemName)
        {
            ItemName = itemName;
        }
        internal void AddItemIntoSlot(InventoryItemButton itemButton)
        {
            Occupied = true;
            ItemName = itemButton.ItemName;
            textSlotLabel.enabled = false;
            itemButton.SetParent(transform);
        }

        internal void RemoveItemFromSlot()
        {
            Occupied = false;
            textSlotLabel.enabled = true;
            ItemName = string.Empty;
        }
    }
}