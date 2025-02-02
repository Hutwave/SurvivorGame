using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum itemType{
    Weapon,
    Secondary,
    Hat,
    Top,
    Bottom,
    Shoe,
    Glove
}



public class Item
{
    public string itemName;
    public int itemLevel;
    public int attack;
    public int defense;
    public int intelligence;
    public itemType itemType;
    public PlayerClass classReq;
    public GameObject itemObject;
    public Sprite img;
}