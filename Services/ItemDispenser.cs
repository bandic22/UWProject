using System;
using UWProject.Models;

public interface IItemDispenser
{
    void DispenseItem(Item item);
}

public class ItemDispenser : IItemDispenser
{
    /// <summary>
    /// Dispenses the selected item.
    /// </summary>
    /// <param name="item">The item to dispense.</param>
    /// <returns>The dispensed item.</returns>
    public void DispenseItem(Item item)
    {
        Console.WriteLine("\nPress any key to dispense order...");
        Console.ReadLine();

        Console.Clear();

        Console.WriteLine($"Dispensing order...\n\nItem: {item.Name}\nItem Code: {item.Id}\nPrice: ${item.Value}\nQuantity Remaining: {item.Quantity}");
    }
}
