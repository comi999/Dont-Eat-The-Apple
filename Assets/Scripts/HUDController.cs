using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public enum UI
{
    INVENTORY,
    STORAGE,
    SHOP
}

public class HUDController : Singleton<HUDController>
{
    Text currencyText;
    public UI currentInterface = UI.INVENTORY;
    public Inventory currentInventory;
    public GameObject inventoryItemEntry;
    public GameObject transferItemEntry;
    private bool uiShowing = false;

    private void Start()
    {
        currencyText = CurrencyViewer.Instance.GetComponent<Text>();

        UpdateCurrency();
    }

    public void UpdateCurrency()
    {
        currencyText.text = "Coins: " + PlayerController.Instance.playerMoney.ToString();
    }

    public void ShowCurrentUI()
    {
        if (uiShowing)
            return;

        switch (currentInterface)
        {
            case UI.INVENTORY:
                InventoryList.Instance.Populate();
                InventoryScreen.Instance.gameObject.SetActive(true);
                break;
            case UI.STORAGE:
                BagStorageList.Instance.Populate();
                StorageList.Instance.Populate(currentInventory);
                StorageScreen.Instance.gameObject.SetActive(true);
                StorageScreen.Instance.OpenScroller();
                break;
            case UI.SHOP:
                BagShopList.Instance.Populate();
                ShopList.Instance.Populate(currentInventory);
                ShopScreen.Instance.gameObject.SetActive(true);
                ShopScreen.Instance.OpenScroller();
                break;
            default:
                break;
        }

        uiShowing = true;
    }

    public void CloseUI()
    {
        if (!uiShowing)
            return;

        switch (currentInterface)
        {
            case UI.INVENTORY:
                InventoryList.Instance.Unpopulate();
                InventoryScreen.Instance.gameObject.SetActive(false);
                break;
            case UI.STORAGE:
                BagStorageList.Instance.Unpopulate();
                StorageList.Instance.Unpopulate();
                StorageScreen.Instance.CloseScroller();
                StorageScreen.Instance.gameObject.SetActive(false);
                break;
            case UI.SHOP:
                BagShopList.Instance.Unpopulate();
                ShopList.Instance.Unpopulate();
                ShopScreen.Instance.CloseScroller();
                ShopScreen.Instance.gameObject.SetActive(false);
                break;
            default:
                break;
        }

        uiShowing = false;
    }

    public void ChangeCurrentInventory(Inventory inventory)
    {
        currentInventory = inventory;
    }

    public bool IsUIShowing()
    {
        return uiShowing;
    }

    public void ChangeUI(UI menu)
    {
        currentInterface = menu;
    }
}
