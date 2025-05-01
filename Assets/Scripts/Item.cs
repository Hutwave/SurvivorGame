using UnityEngine;

public enum ItemType{
    Weapon,
    Secondary,
    Hat,
    Top,
    Bottom,
    Shoe,
    Glove
}
public enum WindowType
{
    Inventory,
    Equipment
}
public class Item
{
    public string itemName;
    public int itemLevel;
    public int attack;
    public int defense;
    public int intelligence;
    public ItemType itemType;
    public PlayerClass classReq;
    public GameObject itemObject;
    public Sprite img;
}