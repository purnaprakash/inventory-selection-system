using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Inventory
{
    public class ItemConfig : ScriptableObject
    {
        public List<Item> items;

        public ItemConfig()
        {
            items = new List<Item>();
        }

#if UNITY_EDITOR
        [MenuItem("Assets/Create/Data/Item data")]
        public static void Create()
        {
            string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath("Assets/ItemData.asset");

            var asset = CreateInstance<ItemConfig>();
            AssetDatabase.CreateAsset(asset, assetPathAndName);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
#endif
    }

    [Serializable]
    public enum ItemClass
    {
        Common,
        Uncommon,
        Rare,
        Legendary,
        Mythical,
    }

    [Serializable]
    public enum ItemSlot
    {
        Weapon,
        Head,
        Body,
        Feet,
    }

    [Serializable]
    public class Item
    {
        public Sprite icon;
        public string item_name;
        public string description;

        public ItemClass item_class;
        public ItemSlot slot;

        public float damage;
        public float defence;
        public float strength;
        public float agility;
        public float intel;
    }
}