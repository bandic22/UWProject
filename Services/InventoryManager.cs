using System.Collections.Generic;
using System.Linq;
using UWProject.Models;

public interface IInventoryManager
{
    bool SetSelectedItem();
    Item GetSelectedItem();
    void ClearSelectedItem(bool reset = false);
    List<Item> GetInventory();
    void AddItem(Item item);
    bool AssessItemStockLevel(Item item);
}

public class InventoryManager : IInventoryManager
{
    private List<Item> Inventory { get; set; }
    private Item? SelectedItem { get; set; }

    public InventoryManager()
    {
        Inventory = new List<Item>();
        SelectedItem = null;
    }

    /// <summary>
    /// Sets the selected item.
    /// </summary>
    /// <returns>True if the item is in stock; otherwise, false.</returns>
    public bool SetSelectedItem()
    {
        Console.WriteLine("\nEnter the code of the item you want to select:");
        string itemId = Console.ReadLine()!;

        var inStockItem = Inventory.Where(item => item.Id == itemId && item.Quantity > 0).FirstOrDefault();

        if (inStockItem != null)
        {
            SelectedItem = inStockItem;
            SelectedItem.Quantity--;
            return true;
        }
        else
        { 
            Console.WriteLine("\nItem not in stock.\n");
            return false;
        }
    }

    /// <summary>
    /// Gets the selected item.
    /// </summary>
    /// <returns>The selected item.</returns>
    public Item GetSelectedItem()
    {
        return SelectedItem!;
    }

    /// <summary>
    /// Clears the selected item.
    /// </summary>
    public void ClearSelectedItem(bool isReset = false)
    {
        if (SelectedItem != null)
        {
            if (!isReset)
            {
                Inventory.First(i => i.Id == SelectedItem.Id).Quantity++;
            }

            SelectedItem = null;
        }
    }

    /// <summary>
    /// Gets the inventory.
    /// </summary>
    /// <returns>The inventory.</returns>
    public List<Item> GetInventory()
    {
        return Inventory;
    }

    /// <summary>
    /// Adds an item to the inventory.
    /// </summary>
    /// <param name="item">The item to add.</param>
    public void AddItem(Item item)
    {
        Inventory.Add(item);
    }

    /// <summary>
    /// Checks if an item is in stock.
    /// </summary>
    /// <param name="item">The item to check.</param>
    /// <returns>True if the item is in stock; otherwise, false.</returns>
    public bool AssessItemStockLevel(Item? item)
    {
        if (item != null && item.Quantity <= 3)
        {
            return true;
        }

        if (SelectedItem != null && SelectedItem.Quantity <= 3)
        {
            return true;
        }

        return false;
    }
}
