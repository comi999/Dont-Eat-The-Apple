public class TransactioHandler : Singleton<TransactioHandler>
{
    public Inventory transferInventory = new Inventory("", 9999);
    public int transferPrice = 0;

    public void SetupTransfer(ref Inventory inventory)
    {
        foreach (ItemStack itemStack in inventory.inventory)
            transferInventory.Insert(new ItemStack(itemStack.id, 0));
    }

    public bool IncrementTransfer(int index)
    {
        //Conditions for a false increment
        //1. Not enough weight capacity in destination
        //2. Not enough to give
        bool bagIsActiveScroller = false;
        int itemId;
        int transactionWeight;

        if (HUDController.Instance.currentInterface == UI.STORAGE)
            bagIsActiveScroller = BagStorageScroller.Instance.gameObject.activeSelf;
        else if (HUDController.Instance.currentInterface == UI.SHOP)
            bagIsActiveScroller = BagShopScroller.Instance.gameObject.activeSelf;

        itemId = transferInventory.inventory[index].id;
        transactionWeight = transferInventory.weight + ItemDatabase.GetItem(itemId).weight;

        if (bagIsActiveScroller)
        {
            //Check if destination has space
            if (HUDController.Instance.currentInventory.weight + transactionWeight > 
                HUDController.Instance.currentInventory.weightLimit)
                return false;

            //Check if source has enough
            int itemCount = PlayerController.Instance.bag.inventory[index].count
                                      - transferInventory.inventory[index].count;

            if (itemCount < 1)
                return false;

            if (HUDController.Instance.currentInterface == UI.SHOP)
                transferPrice += ItemDatabase.GetItem(itemId).basePrice;
        }
        else
        {
            //Check if destination has space
            if (PlayerController.Instance.bag.weight + transactionWeight >
                PlayerController.Instance.bag.weightLimit)
                return false;

            //Check if source has enough
            int itemCount = HUDController.Instance.currentInventory.inventory[index].count
                                                - transferInventory.inventory[index].count;

            if (itemCount < 1)
                return false;

            //If in shop window, return false if not enough funds.
            if (HUDController.Instance.currentInterface == UI.SHOP)
            {
                if (transferPrice + ItemDatabase.GetItem(itemId).basePrice >
                    PlayerController.Instance.playerMoney)
                    return false;

                transferPrice += ItemDatabase.GetItem(itemId).basePrice;
            }
        }

        //If it gets this far, transaction is possible.

        transferInventory.inventory[index].count++;
        transferInventory.itemCount++;
        transferInventory.weight += ItemDatabase.GetItem(itemId).weight;
        return true;
    }

    public bool DecrementTransfer(int index)
    {
        if (transferInventory.inventory[index].count > 0)
        {
            transferInventory.inventory[index].count--;
            transferInventory.itemCount--;
            int itemId = transferInventory.inventory[index].id;
            transferInventory.weight -= ItemDatabase.GetItem(itemId).weight;
            transferPrice -= ItemDatabase.GetItem(itemId).basePrice;
            return true;
        }

        return false;
    }

    public void Transfer(ref Inventory from, ref Inventory to)
    {
        foreach (ItemStack itemStack in transferInventory.inventory)
            from.TransferTo(itemStack, ref to);

        transferInventory.Clear();
        //transferPrice = 0;
    }
}
