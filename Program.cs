using System;

class Program
{
    static void DisplayCart(CartItem[] cart, int cartCount)
    {
        Console.WriteLine("\n--- YOUR CART ---");

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
            Console.WriteLine("\nCART MENU");
            Console.WriteLine("1. Add Item");
            Console.WriteLine("2. View Cart");
            Console.WriteLine("3. Remove Item");
            Console.WriteLine("4. Update Quantity");
            Console.WriteLine("5. Clear Cart");
            Console.WriteLine("6. Checkout");

            Console.Write("Choose: ");
            int choice = int.Parse(Console.ReadLine());

            if (choice == 1)
            {
                foreach (Product p in products) p.DisplayProduct();

                Console.Write("Enter product number: ");
                int pNum = int.Parse(Console.ReadLine());

                Console.Write("Enter quantity: ");
                int qty = int.Parse(Console.ReadLine());

                cart[cartCount++] = new CartItem
                {
                    Product = products[pNum - 1],
                    Quantity = qty
                };

                products[pNum - 1].DeductStock(qty);
            }
            else if (choice == 2)
            {
                DisplayCart(cart, cartCount);
            }
            else if (choice == 3)
            {
                DisplayCart(cart, cartCount);
                Console.Write("Enter item number to remove: ");
                int index = int.Parse(Console.ReadLine()) - 1;

                for (int i = index; i < cartCount - 1; i++)
                    cart[i] = cart[i + 1];

                cartCount--;
            }
            else if (choice == 4)
            {
                DisplayCart(cart, cartCount);
                Console.Write("Enter item number: ");
                int index = int.Parse(Console.ReadLine()) - 1;

                Console.Write("Enter new quantity: ");
                int qty = int.Parse(Console.ReadLine());

                cart[index].Quantity = qty;
            }
            else if (choice == 5)
            {
                cartCount = 0;
                Console.WriteLine("Cart cleared.");
            }
            else if (choice == 6)
            {
                running = false;
            }
        }
    }
}
