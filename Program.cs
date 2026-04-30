using System;

class Program
{
    static void Main()
    {
        Product[] products =
        {
            new Product { Id = 1, Name = "Portable SSD 1TB", Price = 6000, RemainingStock = 7 },
            new Product { Id = 2, Name = "Bluetooth Speaker", Price = 2500, RemainingStock = 9 },
            new Product { Id = 3, Name = "Smartwatch", Price = 7000, RemainingStock = 6 }
        };

        CartItem[] cart = new CartItem[10];
        int cartCount = 0;

        int receiptNumber = 1;
        string continueShopping = "Y";

        while (continueShopping.ToUpper() == "Y")
        {
            Console.WriteLine("\n=== STORE MENU ===");

            foreach (Product p in products)
                p.DisplayProduct();

            Console.Write("Enter product number: ");
            if (!int.TryParse(Console.ReadLine(), out int pNum) || pNum < 1 || pNum > products.Length)
            {
                Console.WriteLine("Invalid product.");
                continue;
            }

            Product selected = products[pNum - 1];

            Console.Write("Enter quantity: ");
            if (!int.TryParse(Console.ReadLine(), out int qty) || qty <= 0)
            {
                Console.WriteLine("Invalid quantity.");
                continue;
            }

            if (!selected.HasEnoughStock(qty))
            {
                Console.WriteLine("Not enough stock.");
                continue;
            }

            cart[cartCount++] = new CartItem
            {
                Product = selected,
                Quantity = qty
            };

            selected.DeductStock(qty);

            Console.Write("Go to cart menu? (Y/N): ");
            string goCart = Console.ReadLine().ToUpper();

            if (goCart != "Y")
                continue;

            bool inCart = true;

            while (inCart)
            {
                Console.WriteLine("\n=== CART MENU ===");
                Console.WriteLine("1.View 2.Update 3.Remove 4.Clear 5.Checkout");
                Console.Write("Choose: ");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Invalid input.");
                    continue;
                }

                if (choice == 1)
                {
                    Console.WriteLine("\n=== CART ===");
                    for (int i = 0; i < cartCount; i++)
                    {
                        Console.WriteLine($"{cart[i].Product.Name} x{cart[i].Quantity} = {cart[i].GetSubtotal()}");
                    }
                }
                else if (choice == 2)
                {
                    Console.Write("Enter item #: ");
                    int index = int.Parse(Console.ReadLine()) - 1;

                    Console.Write("New quantity: ");
                    int newQty = int.Parse(Console.ReadLine());

                    cart[index].Quantity = newQty;
                    Console.WriteLine("Updated.");
                }
                else if (choice == 3)
                {
                    Console.Write("Enter item #: ");
                    int index = int.Parse(Console.ReadLine()) - 1;

                    for (int i = index; i < cartCount - 1; i++)
                        cart[i] = cart[i + 1];

                    cartCount--;
                    Console.WriteLine("Removed.");
                }
                else if (choice == 4)
                {
                    cartCount = 0;
                    Console.WriteLine("Cart cleared.");
                }
                else if (choice == 5)
                {
                    double total = 0;

                    for (int i = 0; i < cartCount; i++)
                        total += cart[i].GetSubtotal();

                    double discount = total >= 5000 ? total * 0.10 : 0;
                    double finalTotal = total - discount;

                    Console.WriteLine("\n=== RECEIPT ===");

                    for (int i = 0; i < cartCount; i++)
                        Console.WriteLine($"{cart[i].Product.Name} x{cart[i].Quantity} = {cart[i].GetSubtotal()}");

                    Console.WriteLine($"Total: {total}");
                    Console.WriteLine($"Discount: {discount}");
                    Console.WriteLine($"Final Total: {finalTotal}");

                    double payment;

                    while (true)
                    {
                        Console.Write("Enter payment: ");
                        payment = double.Parse(Console.ReadLine());

                        if (payment < finalTotal)
                            Console.WriteLine("Insufficient payment.");
                        else break;
                    }

                    double change = payment - finalTotal;

                    Console.WriteLine($"Change: {change}");
                    Console.WriteLine($"Receipt #: {receiptNumber}");

                    DateTime now = DateTime.Now;
                    Console.WriteLine($"Date: {now:MM/dd/yyyy hh:mm:ss tt}");

                    Console.WriteLine("\nLOW STOCK ALERT:");
                    foreach (Product p in products)
                    {
                        if (p.RemainingStock <= 5)
                            Console.WriteLine($"{p.Name} - Remaining: {p.RemainingStock}");
                    }

                    receiptNumber++;
                    cartCount = 0;

                    Console.WriteLine("Checkout complete.");

                    Console.Write("Continue shopping? (Y/N): ");
                    continueShopping = Console.ReadLine();

                    inCart = false;
                }
            }
        }
    }
}
