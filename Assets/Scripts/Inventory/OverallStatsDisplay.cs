using System;
using TMPro;
using UnityEngine;

namespace Inventory
{
    public class OverallStatsDisplay : MonoBehaviour
    {
        const float Tolerance = 0.001f;
        private const string NegativeSignWithColor = "<color=red>";
        private const string PositiveSignWithColor = "<color=yellow>+";

        public TextMeshProUGUI textDamage, textDefence, textStrength, textIntel, textAgility;
        public TextMeshProUGUI textHp, textMana, textDodgeChance, textCriticalRate;

        private ItemAttribute _currentAttribute;
        private ItemAttribute _attributeDifference;

        private void Awake()
        {
            _currentAttribute = new ItemAttribute();
            _attributeDifference = new ItemAttribute();
        }

        public void AddItemAttribute(Item item)
        {
            ItemAttribute attributeToAdd = new ItemAttribute(item);
            _currentAttribute.AddAttribute(attributeToAdd);
            UpdateDataOnUi();
        }

        public void RemoveItemAttribute(Item item)
        {
            ItemAttribute attributeToAdd = new ItemAttribute(item);
            _currentAttribute.SubtractAttribute(attributeToAdd);
            UpdateDataOnUi();
        }

        internal void UpdateDataOnUi()
        {
            textDamage.text = $"{_currentAttribute.Damage:F1}";
            textStrength.text = $"{_currentAttribute.Strength:F1}";
            textIntel.text = $"{_currentAttribute.Intel:F1}";
            textAgility.text = $"{_currentAttribute.Agility:F1}";
            textDefence.text = $"{_currentAttribute.Defence:F1}";
            textHp.text = $"{_currentAttribute.Hp:F1}";
            textMana.text = $"{_currentAttribute.Mana:F1}";
            textDodgeChance.text = $"{_currentAttribute.DodgeChance:F1}";
            textCriticalRate.text = $"{_currentAttribute.CriticalRate:F1}";
        }

        public void DisplayAttributeDifference(Item currentSlotItem, Item selectedItem)
        {
            ItemAttribute attributeDifference = new ItemAttribute
            {
                Damage = selectedItem.damage - currentSlotItem.damage,
                Defence = selectedItem.defence - currentSlotItem.defence,
                Strength = selectedItem.strength - currentSlotItem.strength,
                Intel = selectedItem.intel - currentSlotItem.intel,
                Agility = selectedItem.agility - currentSlotItem.agility
            };
            attributeDifference.CalculateCompositeAttribute();
            UpdateUiWithDifference(attributeDifference);
        }

        public void DisplayAttributeDifference(Item selectedItem)
        {
            ItemAttribute attributeDifference = new ItemAttribute
            {
                Damage = selectedItem.damage,
                Defence = selectedItem.defence,
                Strength = selectedItem.strength,
                Intel = selectedItem.intel,
                Agility = selectedItem.agility
            };
            attributeDifference.CalculateCompositeAttribute();
            UpdateUiWithDifference(attributeDifference);
        }

        private void UpdateUiWithDifference(ItemAttribute diff)
        {
            UpdateDataOnUi();
            if (Math.Abs(diff.Damage) > Tolerance)
                textDamage.text += GetValueWithSign(diff.Damage);
            if (Mathf.Abs(diff.Strength) > Tolerance)
                textStrength.text += GetValueWithSign(diff.Strength);
            if (Mathf.Abs(diff.Intel) > Tolerance)
                textIntel.text += GetValueWithSign(diff.Intel);
            if (Mathf.Abs(diff.Agility) > Tolerance)
                textAgility.text += GetValueWithSign(diff.Agility);
            if (Mathf.Abs(diff.Defence) > Tolerance)
                textDefence.text += GetValueWithSign(diff.Defence);
            if (Mathf.Abs(diff.Hp) > Tolerance)
                textHp.text += GetValueWithSign(diff.Hp);
            if (Mathf.Abs(diff.Mana) > Tolerance)
                textMana.text += GetValueWithSign(diff.Mana);
            if (Mathf.Abs(diff.DodgeChance) > Tolerance)
                textDodgeChance.text += GetValueWithSign(diff.DodgeChance);
            if (Mathf.Abs(diff.CriticalRate) > Tolerance)
                textCriticalRate.text += GetValueWithSign(diff.CriticalRate);
        }

        private static string GetValueWithSign(float value)
        {
            return $" {(value > 0 ? PositiveSignWithColor : NegativeSignWithColor)}{value:F1}";
        }
    }
}