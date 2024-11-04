using System;
using UWProject.Models;

public interface IChangeDispenser
{
    void DispenseChange(Item item, decimal inputAmount);
}

public class ChangeDispenser : IChangeDispenser
{
    /// <summary>
    /// Dispenses the calculated change amount.
    /// </summary>
    /// <param name="item">The item being dispensed.</param>
    /// <param name="inputAmount">The amount of money deposited.</param>
    public void DispenseChange(Item item, decimal inputAmount)
    {
        Console.WriteLine("\nPress any key to dispense change...");

        Console.ReadLine();

        Console.Clear();

        var change = MakeChange(Convert.ToInt32(inputAmount * 100), Convert.ToInt32(item.Value * 100));

        Console.WriteLine($"Dispensing change...\n\nQuarters: {change.Quarters}\nDimes: {change.Dimes}\nNickels: {change.Nickels}");
    }

    /// <summary>
    /// Calculates the change to return to the customer.
    /// </summary>
    /// <param name="centsDeposited">The amount of money deposited.</param>
    /// <param name="sodaCostInCents">The cost of the soda in cents.</param>
    /// <returns>The change amount.</returns>
    private Change MakeChange(int centsDeposited, int sodaCostInCents)
    {
        int changeInCents = centsDeposited - sodaCostInCents;
        Change change = new Change();
        
        while (changeInCents > 0) {
            if (changeInCents >= 25)
            {
                changeInCents -= 25;
                change.Quarters++;
            }
            else if (changeInCents >= 10)
            {
                changeInCents -= 10;
                change.Dimes++;
            }
            else if (changeInCents >= 5)
            {
                changeInCents -= 5;
                change.Nickels++;
            }
            else
            {
                return change;
            }
        }

        return change;
    }
}
