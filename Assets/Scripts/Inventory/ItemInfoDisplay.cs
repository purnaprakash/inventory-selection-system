using TMPro;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Inventory
{
    public class ItemInfoDisplay : UnityEngine.MonoBehaviour
    {
        public Image imageItem;
        public TextMeshProUGUI textName, textDescription, textClass;
        public TextMeshProUGUI textDamage, textDefence, textStrength, textIntel, textAgility;
        public Button buttonEquipOrUnequip;

        private UnityAction<string> _onItemEquippedIntoSlot, _onItemUnequippedIntoSlot;
        private TextMeshProUGUI _textButtonEquipOrUnequip;
        private Item _selectedItem;
        private bool _occupied = false;


        private void Awake()
        {
            _textButtonEquipOrUnequip = buttonEquipOrUnequip.GetComponentInChildren<TextMeshProUGUI>(true);
            buttonEquipOrUnequip.onClick.AddListener(OnClickButton);
            _textButtonEquipOrUnequip.text = "Equip";
            buttonEquipOrUnequip.interactable = false;
        }

        internal void SubscribeEvents(UnityAction<string> onItemEquippedIntoSlot,
            UnityAction<string> onItemUnequippedIntoSlot)
        {
            _onItemEquippedIntoSlot = onItemEquippedIntoSlot;
            _onItemUnequippedIntoSlot = onItemUnequippedIntoSlot;
        }

        internal void DisplayOccupiedItemInfo(Item item)
        {
            _occupied = true;
            UpdateDetails(item);
        }

        internal void DisplaySelectedItemInfo(Item item)
        {
            _occupied = false;
            UpdateDetails(item);
        }

        private void UpdateButtonText()
        {
            string content = _occupied ? "Unequip" : "Equip";
            _textButtonEquipOrUnequip.text = content;
            buttonEquipOrUnequip.interactable = true;
        }

        private void UpdateDetails(Item item)
        {
            UpdateButtonText();
            _selectedItem = item;
            imageItem.sprite = item.icon;
            textName.text = item.item_name;
            textDescription.text = item.description;
            textClass.text = item.item_class.ToString();
            textDamage.text = item.damage.ToString("00");
            textDefence.text = item.defence.ToString("00");
            textStrength.text = item.strength.ToString("00");
            textIntel.text = item.intel.ToString("00");
            textAgility.text = item.agility.ToString("00");
        }

        private void OnClickButton()
        {
            if (_occupied)
                _onItemUnequippedIntoSlot?.Invoke(_selectedItem.item_name);
            else
                _onItemEquippedIntoSlot?.Invoke(_selectedItem.item_name);

            _occupied = !_occupied;
            UpdateButtonText();
        }

        private void OnDestroy()
        {
            buttonEquipOrUnequip.onClick.RemoveAllListeners();
            _onItemEquippedIntoSlot = null;
            _onItemUnequippedIntoSlot = null;
        }
    }
}