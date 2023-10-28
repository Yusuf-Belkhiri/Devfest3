using System;
using UnityEngine;

[Serializable]
public class Item
{
    [Serializable]
    public enum ItemName
    {
        Null,
        
        Branch,
        Stone,
        RawMeat,
        RoastedMeat,
        StoneSword,
        PickAxe,
        CampFire,
        
        WoodenArrow,
        IronIngot,
        IronOre,
        HealthPotion,
        HealingHerb,
        EmptyFlask,
        SteelArmor,
        Leather,
        StoneWall,
        
        FireArrow,
        FireEssence
    }

    [Serializable]
    public struct ItemNameSpriteMaps {

        public ItemName name;
        public Sprite sprite;
    }

    [field: SerializeField] public ItemName Name { get; set; } = ItemName.Null;
    //[field:SerializeField] public Sprite Sprite { get; set; }
    [field:SerializeField] public int Amount { get; set; } = 1;
}
