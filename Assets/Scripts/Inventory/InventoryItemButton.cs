using UnityEngine;
using UnityEngine.Events;
using Image = UnityEngine.UI.Image;

namespace Inventory
{
    public class InventoryItemButton : UnityEngine.UI.Button
    {
        public Image iconImage;
        public Image bgImage;
        public string ItemName { get; private set; }
        public ItemClass ItemClass { get; private set; }
        public ItemSlot ItemSlot { get; private set; }

        private RectTransform _rectTransform;
        private ItemDragHandler _dragHandler;
        private Transform _parent;

        private void Awake()
        {
            _dragHandler = GetComponent<ItemDragHandler>();
            _parent = transform.parent;
        }

        public void SetData(Item item, Color bgColor)
        {
            _rectTransform = GetComponent<RectTransform>();
            iconImage.sprite = item.icon;
            ItemName = item.item_name;
            ItemClass = item.item_class;
            ItemSlot = item.slot;
            bgImage.color = bgColor;
        }

        internal void SubscribeClickEvent(UnityAction<string> onClickAction)
        {
            onClick.AddListener(() => onClickAction(ItemName));
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            onClick.RemoveAllListeners();
        }

        public void EnableDrag()
        {
            _dragHandler.enabled = true;
        }

        public void DisableDrag()
        {
            _dragHandler.enabled = false;
        }

        public void SetParent(Transform parentTransform)
        {
            transform.SetParent(parentTransform);
            _rectTransform.SetAnchorPresetsToStretchAll();
        }

        internal void ResetParent()
        {
            transform.SetParent(_parent);
        }
    }
}