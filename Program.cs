using System;

class Program
{
    static void DisplayCart(CartItem[] cart, int cartCount)
    {
        if (cartCount == 0)
        {
            Console.WriteLine("Cart is empty.");
            return;
        }

        for (int i = 0; i < cartCount; i++)
        {
            Console.WriteLine($"{i + 1}. {cart[i].Product.Name} x {cart[i].Quantity} = {cart[i].GetSubtotal()}");
        }
    }

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

        bool running = true;

        while (running)
        {
            Console.WriteLine("\n1.Add 2.View 3.Remove 4.Update 5.Clear 6.Checkout");
            int choice = int.Parse(Console.ReadLine());

            if (choice == 1)
            {
                foreach (Product p in products) p.DisplayProduct();

                int pNum = int.Parse(Console.ReadLine());
                int qty = int.Parse(Console.ReadLine());

                cart[cartCount++] = new CartItem
                {
                    Product = products[pNum - 1],
                    Quantity = qty
                };

                products[pNum - 1].DeductStock(qty);
            }
            else if (choice == 6)
            {
                double total = 0;

                for (int i = 0; i < cartCount; i++)
                    total += cart[i].GetSubtotal();

                double discount = (total >= 5000) ? total * 0.10 : 0;
                double finalTotal = total - discount;

                Console.WriteLine($"Final Total: {finalTotal}");

                double payment;

                while (true)
                {
                    Console.Write("Enter payment: ");

                    if (!double.TryParse(Console.ReadLine(), out payment))
                    {
                        Console.WriteLine("Invalid input.");
                        continue;
                    }

                    if (payment < finalTotal)
                    {
                        Console.WriteLine("Insufficient payment.");
                        continue;
                    }

                    break;
                }

                double change = payment - finalTotal;

                Console.WriteLine("\n===== RECEIPT =====");
                Console.WriteLine($"Receipt No: {receiptNumber:D4}");
                Console.WriteLine($"Date: {DateTime.Now}");

                for (int i = 0; i < cartCount; i++)
                {
                    Console.WriteLine($"{cart[i].Product.Name} x {cart[i].Quantity} = {cart[i].GetSubtotal()}");
                }

                Console.WriteLine($"Grand Total: {total}");
                Console.WriteLine($"Discount: {discount}");
                Console.WriteLine($"Final Total: {finalTotal}");
                Console.WriteLine($"Payment: {payment}");
                Console.WriteLine($"Change: {change}");

                receiptNumber++;

                // ✅ LOW STOCK ALERT ADDED HERE
                Console.WriteLine("\nLOW STOCK ALERT:");
                foreach (Product p in products)
                {
                    if (p.RemainingStock <= 5)
                    {
                        Console.WriteLine($"{p.Name} has only {p.RemainingStock} left.");
                    }
                }

                running = false;
            }
        }
    }
}
