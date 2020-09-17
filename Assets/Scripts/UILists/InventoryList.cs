using UnityEngine;

public class InventoryList : Singleton<InventoryList>
{
    public void Populate()
    {
        int i = 0;
        foreach (ItemStack itemStack in PlayerController.Instance.bag.inventory)
        {
            var entry = Instantiate(HUDController.Instance.inventoryItemEntry, Instance.gameObject.transform);
            var entryReferences = entry.GetComponent<ItemEntryReferences>();

            Item item = ItemDatabase.GetItem(itemStack.id);
            entryReferences.index = i++;
            entryReferences.itemName.text = item.title;
            entryReferences.itemCount.text = itemStack.count.ToString() + 'x';
            entryReferences.itemIcon.sprite = item.icon;
            entryReferences.cartAmount = 0;
        }
    }

    public void Unpopulate()
    {
        foreach (Transform itemEntry in Instance.gameObject.gameObject.transform)
            Destroy(itemEntry.gameObject);
    }
}
