using UWProject.Models;

Console.WriteLine("Press any key to turn the vending machine on...");
Console.ReadLine();

IInventoryManager inventoryManager = new InventoryManager();
IPaymentProcessor paymentProcessor = new PaymentProcessor();
IItemDispenser itemDispenser = new ItemDispenser();
IChangeDispenser changeDispenser = new ChangeDispenser();
INotificationService notificationService = new AdminNotificationService();

VendingMachine vendingMachine = new VendingMachine(inventoryManager, paymentProcessor, itemDispenser, changeDispenser, notificationService);

var userSession = false;

do
{
    vendingMachine.Initialize();

    vendingMachine.SelectItem();

    vendingMachine.HandlePayment();

    vendingMachine.DispenseItem();

    vendingMachine.DispenseChange();

    userSession = vendingMachine.Reset();

} while (userSession);
