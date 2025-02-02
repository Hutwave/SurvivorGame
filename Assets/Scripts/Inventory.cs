using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject invSlotPanel;
    public GameObject invSlot;
    public GameLogic gameLogic;

    private void Start()
    {
        gameLogic = FindAnyObjectByType<GameLogic>();
    }

    public void updateItems(List<Item> items)
    {
        foreach(Transform child in invSlotPanel.transform)
        {
            Destroy(child.gameObject);
        }
        foreach(Item slot in items)
        {
            var aa = Instantiate(invSlot);
            aa.name = slot.itemName;
            aa.transform.SetParent(invSlotPanel.transform);
            aa.transform.GetChild(0).GetComponent<Image>().sprite = slot.img;
            aa.transform.GetComponentInChildren<Button>().onClick.AddListener(() => clickItem(slot));
        }
    }


    public void clickItem(Item item)
    {
        Debug.Log(item.itemName);
        gameLogic.equipItem(item);
    }
}
