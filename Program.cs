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
            new Product { Id = 1, Name = "Laptop", Price = 30000, RemainingStock = 5 },
            new Product { Id = 2, Name = "Mouse", Price = 500, RemainingStock = 10 },
            new Product { Id = 3, Name = "Keyboard", Price = 1200, RemainingStock = 7 }
        };

        CartItem[] cart = new CartItem[10];
        int cartCount = 0;

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
                Console.WriteLine($"Change: {change}");

                running = false;
            }
        }
    }
}
