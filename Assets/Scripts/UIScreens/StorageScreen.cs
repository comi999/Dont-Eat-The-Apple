using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StorageScreen : Singleton<StorageScreen>
{
    public Button transferButton;
    
    public void OpenScroller()
    {
        StorageScroller.Instance.gameObject.SetActive(true);
        BagStorageScroller.Instance.gameObject.SetActive(false);

        transferButton.interactable = false;

        TransactioHandler.Instance.SetupTransfer(ref HUDController.Instance.currentInventory);
    }

    public void CloseScroller()
    {
        StorageScroller.Instance.gameObject.SetActive(false);
        BagStorageScroller.Instance.gameObject.SetActive(false);
    }

    public void ToggleInventoryView()
    {
        bool currentState = StorageScroller.Instance.gameObject.activeSelf;
        StorageScroller.Instance.gameObject.SetActive(!currentState);
        BagStorageScroller.Instance.gameObject.SetActive(currentState);
        
        TransactioHandler.Instance.transferInventory.Clear();

        if (currentState)
        {
            TransactioHandler.Instance.SetupTransfer(ref PlayerController.Instance.bag);

            foreach (Transform entry in StorageList.Instance.gameObject.transform)
            {
                var itemRef = entry.GetComponent<ItemEntryReferences>();
                itemRef.cartCount.text = "0";
                itemRef.cartAmount = 0;
            }
        }
        else
        {
            TransactioHandler.Instance.SetupTransfer(ref HUDController.Instance.currentInventory);

            foreach (Transform entry in BagStorageList.Instance.gameObject.transform)
            {
                var itemRef = entry.GetComponent<ItemEntryReferences>();
                itemRef.cartCount.text = "0";
                itemRef.cartAmount = 0;
            }
        }
    }
}
