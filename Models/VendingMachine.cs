using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWProject.Models
{
    public class VendingMachine
    {
        private IInventoryManager InventoryManager { get; set; }
        private IPaymentProcessor PaymentProcessor { get; set; }
        private IItemDispenser ItemDispenser { get; set; }
        private IChangeDispenser ChangeDispenser { get; set; }
        private INotificationService AdminNotificationService { get; set; }

        public VendingMachine(
            IInventoryManager inventoryManager,
            IPaymentProcessor paymentProcessor,
            IItemDispenser itemDispenser,
            IChangeDispenser changeDispenser,
            INotificationService adminNotificationService)
        {
            InventoryManager = inventoryManager;
            PaymentProcessor = paymentProcessor;
            ItemDispenser = itemDispenser;
            ChangeDispenser = changeDispenser;
            AdminNotificationService = adminNotificationService;

            List<Item> items = new List<Item>()
        {
            new Item("001", "Pepsi", 1.25m, 10),
            new Item("002", "Coke", 1.25m, 0),
            new Item("003", "Starry", 1.65m, 4),
            new Item("004", "7up", 2m, 4),
            new Item("005", "Sprite", 2.80m, 1)
        };

            foreach (var item in items)
            {
                bool lowStock = InventoryManager.AssessItemStockLevel(item);

                if (lowStock)
                {
                    AdminNotificationService.AddNotification($"{item.Name} is low on stock.");
                }

                InventoryManager.AddItem(item);
            }

            Console.WriteLine("Vending Machine Online");
        }

        /// <summary>
        /// Initializes the vending machine.
        /// </summary>
        public void Initialize()
        {
            AdminNotificationService.SendAllNotifications();

            Console.WriteLine("\nInventory:");

            foreach (var item in GetInventory())
            {
                Console.WriteLine($"\nCode: {item.Id}: {item.Name}\nPrice: ${item.Value}\nQuantity: {item.Quantity}");
            }
        }

        /// <summary>
        /// Selects an item from the inventory.
        /// </summary>
        public void SelectItem()
        {
            var success = InventoryManager.SetSelectedItem();

            if (!success)
            {
                SelectItem();
            }
        }

        /// <summary>
        /// Gets the selected item.
        /// </summary>
        public void GetSelectedItem()
        {
            Console.WriteLine($"\nSelected item: {InventoryManager.GetSelectedItem().Name}");
        }

        /// <summary>
        /// Handles the payment for the selected item.
        /// </summary>
        public void HandlePayment()
        {
            var paymentSuccess = PaymentProcessor.ProcessPayment(InventoryManager.GetSelectedItem(), out bool isSystemError);

            if (!paymentSuccess)
            {
                if (isSystemError)
                {
                    PaymentProcessor.RefundPayment();
                }

                HandlePayment();
            }
        }

        /// <summary>
        /// Dispenses the change for the selected item.
        /// </summary>
        public void DispenseChange()
        {
            ChangeDispenser.DispenseChange(InventoryManager.GetSelectedItem(), PaymentProcessor.GetAmountPaid());
        }

        /// <summary>
        /// Dispenses the selected item.
        /// </summary>
        public void DispenseItem()
        {
            Item item = InventoryManager.GetSelectedItem();

            ItemDispenser.DispenseItem(item);

            if (InventoryManager.AssessItemStockLevel(item))
            {
                AdminNotificationService.AddNotification($"{item.Name} is low on stock.");
            }
        }

        /// <summary>
        /// Gets the inventory.
        /// </summary>
        public List<Item> GetInventory()
        {
            return InventoryManager.GetInventory();
        }

        /// <summary>
        /// Resets the vending machine for a new order.
        /// </summary>
        public bool Reset()
        {
            InventoryManager.ClearSelectedItem(true);
            PaymentProcessor.ClearAmountPaid();

            Console.WriteLine("\nWould you like to select another item? (y/n)");

            return Console.ReadLine() == "y";
        }
    }
}
