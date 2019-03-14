using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Inventory
{
    [RequireComponent(typeof(CanvasGroup))]
    public class ItemDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public static InventoryItemButton selectedItemButton;
        private CanvasGroup _canvasGroup;
        private Transform _parentDuringMove;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _parentDuringMove = GetComponentInParent<ScrollRect>().transform.parent;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            selectedItemButton = GetComponent<InventoryItemButton>();
            transform.SetParent(_parentDuringMove);
             _canvasGroup.blocksRaycasts = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _canvasGroup.blocksRaycasts = true;
            if (transform.parent == _parentDuringMove)
                selectedItemButton.ResetParent();
        }
    }
}