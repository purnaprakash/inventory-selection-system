namespace Inventory
{
    public struct ItemAttribute
    {
        public float Damage, Defence, Strength, Intel, Agility, Hp, Mana, DodgeChance, CriticalRate;

        public ItemAttribute(float damage, float defence, float strength, float intel, float agility)
        {
            Damage = damage;
            Defence = defence;
            Strength = strength;
            Intel = intel;
            Agility = agility;
            Hp = Strength * 12;
            Mana = Intel * 14;
            DodgeChance = Agility * 0.2f;
            CriticalRate = Agility * 0.15f;
        }

        public ItemAttribute(Item item)
        {
            Damage = item.damage;
            Defence = item.defence;
            Strength = item.strength;
            Intel = item.intel;
            Agility = item.agility;
            Hp = Strength * 12;
            Mana = Intel * 14;
            DodgeChance = Agility * 0.2f;
            CriticalRate = Agility * 0.15f;
        }

        public void AddAttribute(ItemAttribute itemAttribute)
        {
            Damage += itemAttribute.Damage;
            Defence += itemAttribute.Defence;
            Strength += itemAttribute.Strength;
            Intel += itemAttribute.Intel;
            Agility += itemAttribute.Agility;
            CalculateCompositeAttribute();
        }

        public void SubtractAttribute(ItemAttribute itemAttribute)
        {
            Damage -= itemAttribute.Damage;
            Defence -= itemAttribute.Defence;
            Strength -= itemAttribute.Strength;
            Intel -= itemAttribute.Intel;
            Agility -= itemAttribute.Agility;
            CalculateCompositeAttribute();
        }

        public void CalculateCompositeAttribute()
        {
            Hp = Strength * 12;
            Mana = Intel * 14;
            DodgeChance = Agility * 0.2f;
            CriticalRate = Agility * 0.15f;
        }

        public static ItemAttribute operator -(ItemAttribute item1, ItemAttribute item2)
        {
            ItemAttribute itemAttribute = new ItemAttribute
            {
                Damage = item1.Damage - item2.Damage,
                Defence = item1.Defence - item2.Defence,
                Strength = item1.Strength - item2.Strength,
                Intel = item1.Intel - item2.Intel,
                Agility = item1.Agility - item2.Agility
            };
            itemAttribute.CalculateCompositeAttribute();
            return itemAttribute;
        }
    }
}