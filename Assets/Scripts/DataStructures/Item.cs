using UnityEngine;

public class ItemStack
{
    public int id;
    public int count;

    public ItemStack(int id, int count)
    {
        this.id = id;
        this.count = count;
    }

    public int GetWeight()
    {
        Item item = ItemDatabase.GetItem(id);

        if (item == null)
            return 0;

        return item.weight * count;
    }
}

public class Item
{
    public int id;
    public int weight;
    public int basePrice;
    public int marketPrice = 0;
    public string title;
    public string description;
    public Sprite icon;

    public Item(int id, int weight, int basePrice, string title, string description)
    {
        this.id = id;
        this.weight = weight;
        this.basePrice = basePrice;
        this.title = title;
        this.description = description;
        icon = Resources.Load<Sprite>("Sprites/Items/" + title);
    }

    public Item(Item item)
    {
        id = item.id;
        weight = item.weight;
        basePrice = item.basePrice;
        title = item.title;
        description = item.description;
        icon = Resources.Load<Sprite>("Sprites/Items/" + item.title);
    }
}
