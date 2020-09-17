using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ItemDatabase
{
    public static List<Item> items = new List<Item>() 
    {
        //Fruit items.
        new Item(0,  1, 2,  "Apple", "A shiny red apple."),
        new Item(1,  1, 2,  "Orange", "An orange....orange."),
        new Item(2,  1, 2,  "Blueberries", "Keep away from white shirts."),
        new Item(3,  1, 3,  "Banana", "Who bends these?"),
        new Item(4,  1, 2,  "Grapes", "A small bundle of grapes."),
        new Item(5,  1, 1,  "Strawberry", "Not actually a berry."),
        //Hardware items.                                                                                              
        new Item(6,  2,  5,  "Nails", "A small handful of nails."),
        new Item(7,  50, 15, "Timber", "A single 2m plank of timber."),
        new Item(8,  5,  8,  "Glue", "Many horses died for this."),
        new Item(9,  30, 30, "Hammer", "A trusty iron hammer."),
        new Item(10, 30, 30, "Saw", "A sharp and reliable wood saw."),
        new Item(11, 5,  25, "Cedar Oil", "Half a pint of an expensive oil used to scent timber."),
        new Item(12, 8,  20, "Lacquer", "A small half pint tin of lacquer used to bring out features in timber."),
        //Bakery items.
        new Item(13, 2,  3,  "Bread", "A decently sized loaf of bread."),
        new Item(14, 1,  8,  "Croissant", "A pastry of French origin."),
        new Item(15, 6,  10, "Cherry Jam", "Homemade jam made from imported cherries."),
        new Item(16, 8,  15, "Apple Pie", "Homemade apple pie with a criss-crossed top"),
        new Item(17, 5,  20, "Honey", "Honey imported from Blacksburry."),
        new Item(18, 20, 20, "Cake", "Round cake made of two layers, covered in frosting."),
        //Fine goods.                                             
        new Item(19, 4,  30, "Coffee", "A small jar of a rare aromatic bean imported from far away."),
        new Item(20, 4,  35, "Tea", "Imported tea leaves in a small crate. A must have for the wealthy."),
        new Item(21, 5,  90, "Spices", "A small jar of rare spices."),
        new Item(22, 3,  10, "Candle", "A scented wax candle."),
        new Item(23, 1,  95, "Silk", "A small roll of fine silk."),
        new Item(24, 1,  130,"Pearl", "A small shiny pearl."),
        new Item(25, 1,  110, "Incense", "A small cotton bag of powdered aromatic resin."),
        //Delicatessen items.                                                                                          
        new Item(26, 3,  15, "Blue Cheese", "A wax covered block of mouldy cheese."),
        new Item(27, 3,  15, "Smoked Meat", "Which animal did this come from?"),
        new Item(28, 3,  10, "Nuts", "A small bag of assorted nuts"),
        new Item(29, 4,  25, "Olive Oil", "A glass bottle filled with olive oil."),
        new Item(30, 2,  15, "Herring", "Little pickled fish in brine.")
    };

    static public Item GetItem(int id)
    {
        if (id < 0 || id >= items.Count)
            return null;

        return items[id];
    }

    static public Item GetItem(string title)
    {
        return items.Find(item => item.title == title);
    }
}
