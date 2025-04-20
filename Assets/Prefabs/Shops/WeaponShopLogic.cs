using NUnit.Framework;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.UI;

public class WeaponShopLogic : MonoBehaviour
{
    public GameObject ItemArea;
    public GameObject ItemSlot;
    public ItemShop parentObj;
    public System.Collections.Generic.List<GameObject> items;



    public void setHandler(ItemShop itemShop)
    {
        parentObj = itemShop;
    }

    public void clearItems()
    {
        items.ForEach(oneItem => Destroy(oneItem));
        items.Clear();
    }

    public void addItem(Item item)
    {
        var slot = Instantiate(ItemSlot);
        items.Add(slot);
        slot.transform.SetParent(ItemArea.transform);
        slot.name = item.itemName;
        slot.transform.GetChild(0).GetComponent<Image>().sprite = item.img;
        slot.GetComponentInChildren<Button>().onClick.AddListener(() => parentObj.itemBought(item));
    }
}
