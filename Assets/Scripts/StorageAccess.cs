using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using UnityEngine;

public class StorageAccess : MonoBehaviour
{
    private Inventory storage;

    public  string name = "Storage";
    public  int  weightLimit = 5;

    private void Awake()
    {
        storage = new Inventory(name, weightLimit);

        storage.Insert(new ItemStack(0, 1));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            HUDController.Instance.ChangeUI(UI.STORAGE);
            HUDController.Instance.ChangeCurrentInventory(storage);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
            HUDController.Instance.ChangeUI(UI.INVENTORY);
    }
}
