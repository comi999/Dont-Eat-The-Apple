using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopAccess : MonoBehaviour
{
    public string shopName = "Shop";

    private Inventory shopItems;
    private Inventory itemWhiteList;

    public int[] whiteListID = { -1 };

    private void Awake()
    {
        shopItems = new Inventory(shopName, 10000);
        itemWhiteList = new Inventory("", 10000);

        foreach (int id in whiteListID)
            itemWhiteList.Insert(new ItemStack(id, 0));

        shopItems.Insert(new ItemStack(0, 10));
        shopItems.Insert(new ItemStack(1, 10));
        shopItems.Insert(new ItemStack(2, 10));
        shopItems.Insert(new ItemStack(3, 10));
        shopItems.Insert(new ItemStack(4, 10));
        shopItems.Insert(new ItemStack(5, 10));

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            HUDController.Instance.ChangeUI(UI.SHOP);
            HUDController.Instance.ChangeCurrentInventory(shopItems);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
            HUDController.Instance.ChangeUI(UI.INVENTORY);
    }
}
