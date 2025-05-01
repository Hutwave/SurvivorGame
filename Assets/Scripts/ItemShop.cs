using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class ItemShop : MonoBehaviour
{
    public float everySecond;
    GameLogic gameLogic;
    public List<Item> items = new List<Item>();
    public GameObject weaponShop;
    public WeaponShopLogic weaponShopLogic;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameLogic = FindAnyObjectByType<GameLogic>();
        weaponShop = Instantiate(weaponShop);
        generateItems(null,0,0,0);
        weaponShopLogic = weaponShop.transform.GetComponent<WeaponShopLogic>();
        weaponShopLogic.setHandler(this);
        createShop();
        everySecond = 60f;
    }

    // Update is called once per frame
    void TestShops()
    {
        bool playerFound = false;
        var player = Physics.OverlapSphere(transform.position, 7.5f, LayerMask.GetMask("Default"));
        if (player.Length > 0)
        {
            foreach (var item in player)
            {
                if (item.CompareTag("Player"))
                {
                    float playerRange = Vector3.Distance(this.transform.position, item.ClosestPoint(this.transform.position));
                    if(gameLogic.showShop(this, playerRange, true))
                    {
                        this.weaponShop.SetActive(true);
                    }
                    playerFound = true;

                }
            }
        }
        if (!playerFound)
        {
            if(!gameLogic.showShop(this, 100f, false))
            {
                this.weaponShop.SetActive(false);
            }
        }
            
    }

    private void FixedUpdate()
    {
        everySecond += Time.deltaTime;
        if (everySecond > 0.25f)
        {
            TestShops();
            everySecond = 0f;
        }
        
    }

    public void createShop()
    {
        foreach (var item in items)
        {
            weaponShopLogic.addItem(item);
        }
    }

    public void itemBought(Item item)
    {
        if (gameLogic.getMesos() > item.attack)
        {
            gameLogic.addMesos(-item.attack);
            items.Remove(item);
            gameLogic.equipItem(item);
            weaponShopLogic.clearItems();
            createShop();
        }
        else gameLogic.addMesos(5);
    }

    public void generateItems(List<ItemType> types, int amount, int level, int rarity)
    {
        items.Add(Addressables.LoadAssetAsync<ItemConfig>("Assets/Items/Item2.asset").WaitForCompletion().getItem());
        items.Add(Addressables.LoadAssetAsync<ItemConfig>("Assets/Items/Item2.asset").WaitForCompletion().getItem());
        items.Add(Addressables.LoadAssetAsync<ItemConfig>("Assets/Items/Hats/Mage10.asset").WaitForCompletion().getItem());
    }

}
