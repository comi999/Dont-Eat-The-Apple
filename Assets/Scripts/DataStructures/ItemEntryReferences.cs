using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemEntryReferences : MonoBehaviour
{
    public int index;
    public Text  itemName;
    public Text  itemCount;
    public Text  cartCount;
    public Image itemIcon;
    public Image entryImage;

    public int cartAmount = 0;
    float failTimer = 0;

    public void TriggerIncrement()
    {
        if (TransactioHandler.Instance.IncrementTransfer(index))
        {
            cartAmount++;
            cartCount.text = cartAmount.ToString();
        }
        else
        {
            failTimer = 0.3f;
            cartCount.color = Color.red;
        }

        bool cartHasItems = TransactioHandler.Instance.transferInventory.itemCount > 0;
        ShopScreen.Instance.buySellButton.interactable = cartHasItems;
        StorageScreen.Instance.transferButton.interactable = cartHasItems;
    }

    public void TriggerDecrement()
    {
        if (TransactioHandler.Instance.DecrementTransfer(index))
        {
            cartAmount--;
            cartCount.text = cartAmount.ToString();
        }
        else
        {
            failTimer = 0.3f;
            cartCount.color = Color.red;
        }

        bool cartHasItems = TransactioHandler.Instance.transferInventory.itemCount > 0;
        ShopScreen.Instance.buySellButton.interactable = cartHasItems;
        StorageScreen.Instance.transferButton.interactable = cartHasItems;
    }

    public void TriggerTransfer()
    {
        if (HUDController.Instance.currentInterface == UI.STORAGE || 
            HUDController.Instance.currentInterface == UI.SHOP)
        {
            bool bagIsActiveScroller;

            if (HUDController.Instance.currentInterface == UI.STORAGE)
                bagIsActiveScroller = BagStorageScroller.Instance.gameObject.activeSelf;
            else
                bagIsActiveScroller = BagShopScroller.Instance.gameObject.activeSelf;

            if (bagIsActiveScroller)
            {
                TransactioHandler.Instance.Transfer(ref PlayerController.Instance.bag,
                                                    ref HUDController.Instance.currentInventory);

                TransactioHandler.Instance.transferInventory.Clear();
                TransactioHandler.Instance.SetupTransfer(ref PlayerController.Instance.bag);
            }
            else
            {
                TransactioHandler.Instance.Transfer(ref HUDController.Instance.currentInventory,
                                                    ref PlayerController.Instance.bag);

                TransactioHandler.Instance.transferInventory.Clear();
                TransactioHandler.Instance.SetupTransfer(ref HUDController.Instance.currentInventory);
            }

            if (HUDController.Instance.currentInterface == UI.STORAGE)
            {
                BagStorageList.Instance.Unpopulate();
                BagStorageList.Instance.Populate();
                StorageList.Instance.Unpopulate();
                StorageList.Instance.Populate(HUDController.Instance.currentInventory);
            }
            else
            {
                BagShopList.Instance.Unpopulate();
                BagShopList.Instance.Populate();
                ShopList.Instance.Unpopulate();
                ShopList.Instance.Populate(HUDController.Instance.currentInventory);
                
                //Change player money upon sell if UI is SHOP
                if (bagIsActiveScroller)
                {
                    //Sell
                    PlayerController.Instance.playerMoney +=
                        TransactioHandler.Instance.transferPrice;

                    TransactioHandler.Instance.transferPrice = 0;
                }
                else
                {
                    //Buy
                    PlayerController.Instance.playerMoney -=
                        TransactioHandler.Instance.transferPrice;

                    TransactioHandler.Instance.transferPrice = 0;
                }

                HUDController.Instance.UpdateCurrency();
            }
        }
    }

    private void Update()
    {
        if (failTimer > 0)
        {
            failTimer -= Time.deltaTime;

            if (failTimer <= 0)
                cartCount.color = Color.black;
        }
    }
}
