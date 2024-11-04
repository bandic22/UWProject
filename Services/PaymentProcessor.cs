using System;
using UWProject.Models;

public interface IPaymentProcessor
{
    bool ProcessPayment(Item item, out bool isSystemError);
    void RefundPayment();
    decimal GetAmountPaid();
    void ClearAmountPaid();
}

public class PaymentProcessor : IPaymentProcessor
{
    private decimal cashAmount = 0;

    /// <summary>
    /// Processes the payment for a selected item.
    /// </summary>
    /// <param name="item">The item being purchased.</param>
    /// <returns>True if payment is successful; otherwise, false.</returns>
    public bool ProcessPayment(Item item, out bool isSystemError)
    {
        isSystemError = false;

        Console.WriteLine("Enter the amount of cash you want to pay in whole dollars:");

        string amount = Console.ReadLine()!;

        Console.WriteLine("\nVerifying payment...");

        bool systemError = new Random().NextDouble() < 0.5;

        cashAmount = Convert.ToDecimal(amount);

        if (cashAmount >= item.Value && !systemError)
        {
            Console.WriteLine("\nPayment successful!");
            return true;
        } 
        else if (systemError)
        {
            Console.WriteLine("\nPayment system failed!");
            isSystemError = true;
            return false;
        }
        else
        {
            Console.WriteLine("\nInsufficient funds!");
            return false;
        }
    }

    /// <summary>
    /// Refunds the customer's payment.
    /// </summary>
    public void RefundPayment()
    {
        Console.WriteLine($"\nRefunding: ${cashAmount}");
        cashAmount = 0;
    }

    /// <summary>
    /// Gets the amount of cash paid by the customer.
    /// </summary>
    /// <returns>The amount of cash paid by the customer.</returns>
    public decimal GetAmountPaid()
    {
        return cashAmount;
    }

    /// <summary>
    /// Clears the amount of cash paid by the customer.
    /// </summary>
    public void ClearAmountPaid()
    {
        cashAmount = 0;
    }
}

