using UnityEngine;
using UnityEngine.EventSystems;

namespace Inventory
{
    public class ItemDropHandler : UnityEngine.MonoBehaviour, IDropHandler
    {
        private SlotDetail _slotDetail;

        private void Awake()
        {
            _slotDetail = GetComponent<SlotDetail>();
        }

        public void OnDrop(PointerEventData eventData)
        {
            InventoryItemButton itemButton = ItemDragHandler.selectedItemButton;
            if (CanMoveToSlot())
            {
                if (!string.IsNullOrEmpty(_slotDetail.ItemName))
                {
                    //_slotDetail.RemoveItemFromSlot();
                    InventoryManager.Instance.MoveItemToInventory(_slotDetail.ItemName);
                }

                string itemName = itemButton.ItemName;
                _slotDetail.AddItemIntoSlot(itemButton);
                itemButton.DisableDrag();
                InventoryManager.Instance.AddAttributeIntoOverallStats(itemName);
            }
            else
            {
                itemButton.ResetParent();
            }

            itemButton.GetComponent<CanvasGroup>().blocksRaycasts = true;
            ItemDragHandler.selectedItemButton = null;
        }

        private bool CanMoveToSlot()
        {
            return ItemDragHandler.selectedItemButton != null &&
                   _slotDetail.slotType == ItemDragHandler.selectedItemButton.ItemSlot;
        }
    }
}