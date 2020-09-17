using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScreen : Singleton<ShopScreen>
{
    public Button buySellButton;
    public Text buySellText;

    public void OpenScroller()
    {
        ShopScroller.Instance.gameObject.SetActive(true);
        BagShopScroller.Instance.gameObject.SetActive(false);

        buySellText.text = "Buy All";
        buySellButton.interactable = false;

        TransactioHandler.Instance.SetupTransfer(ref HUDController.Instance.currentInventory);
    }

    public void CloseScroller()
    {
        ShopScroller.Instance.gameObject.SetActive(false);
        BagShopScroller.Instance.gameObject.SetActive(false);
    }

    public void ToggleInventoryView()
    {
        bool currentState = ShopScroller.Instance.gameObject.activeSelf;
        ShopScroller.Instance.gameObject.SetActive(!currentState);
        BagShopScroller.Instance.gameObject.SetActive(currentState);

        TransactioHandler.Instance.transferInventory.Clear();
        TransactioHandler.Instance.transferPrice = 0;

        if (currentState)
            buySellText.text = "Sell All";
        else
            buySellText.text = "Buy All";

        if (currentState)
        {
            TransactioHandler.Instance.SetupTransfer(ref PlayerController.Instance.bag);

            foreach (Transform entry in ShopList.Instance.gameObject.transform)
            {
                var itemRef = entry.GetComponent<ItemEntryReferences>();
                itemRef.cartCount.text = "0";
                itemRef.cartAmount = 0;
            }
        }
        else
        {
            TransactioHandler.Instance.SetupTransfer(ref HUDController.Instance.currentInventory);

            foreach (Transform entry in BagShopList.Instance.gameObject.transform)
            {
                var itemRef = entry.GetComponent<ItemEntryReferences>();
                itemRef.cartCount.text = "0";
                itemRef.cartAmount = 0;
            }
        }
    }

    public void InitialiseShopPrices()
    {

    }
}
