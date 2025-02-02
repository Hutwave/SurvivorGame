using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Equips : MonoBehaviour
{
    Item hat;
    Item top;
    Item bottom;
    Item shoe;
    Item glove;
    Item weapon;
    Item secondary;

    public GameObject invSlot;
    GameLogic gameLogic;
    public GameObject hatPanel;
    public GameObject topPanel;
    public GameObject bottomPanel;
    public GameObject shoePanel;
    public GameObject glovePanel;
    public GameObject weaponPanel;
    public GameObject secondaryPanel;


    // Start is called before the first frame update
    void Start()
    {
        gameLogic = FindAnyObjectByType<GameLogic>();
        weaponPanel = this.gameObject.transform.Find("EquipmentModal").Find("WeaponBar").Find("WeaponPanel").gameObject;
        //var aa = this.gameObject.transform.Find("WeaponBar").Find("WeaponPanel");
    }

    public void updateItems(List<Item> items)
    {
        
        items.ForEach(x =>
        {
            equipItem(x);
        });
    }

    void updateIcon(GameObject panel, Sprite img)
    {

        /*
        foreach (Transform child in panel.transform)
        {
            Destroy(child.gameObject);
        }
        //var aa = Instantiate(invSlot);
        */
        panel.transform.GetChild(0).GetComponent<Image>().sprite = img;
        //aa.transform.SetParent(weaponPanel.transform);
        
        return;
    }

    public void equipItem(Item item)
    {
        Item tempItem = new Item();
        switch (item.itemType)
        {
            case itemType.Hat:
                updateIcon(hatPanel, item.img);
                return;
            case itemType.Top:
                updateIcon(topPanel, item.img);
                return;
            case itemType.Bottom:
                updateIcon(bottomPanel, item.img);
                return;
            case itemType.Glove:
                updateIcon(glovePanel, item.img);
                return;
            case itemType.Shoe:
                updateIcon(shoePanel, item.img);
                return;
            case itemType.Weapon:
                weapon = item;
                updateIcon(weaponPanel, item.img);
                return;
            case itemType.Secondary:
                secondary = item;
                updateIcon(secondaryPanel, item.img);
                return;
            default:
                {
                    return;
                }

        }
    }
}
