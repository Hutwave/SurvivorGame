using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Data", menuName = "Item", order = 1)]
public class ItemConfig : ScriptableObject
{
    public string itemName;
    public int itemId;
    public int itemLevel;
    public int attack;
    public int defense;
    public int intelligence;
    public itemType itemType;
    public PlayerClass classReq;
    public GameObject itemObject;
    public Sprite img;

    public Item getItem()
    {
        return new Item()
        {
            attack = this.attack,
            itemLevel = this.itemLevel,
            defense = this.defense,
            intelligence = this.intelligence,
            classReq = PlayerClass.Magician,
            itemObject = this.itemObject,
            itemName = this.itemName + this.itemId,
            img = this.img
        };
    }
}