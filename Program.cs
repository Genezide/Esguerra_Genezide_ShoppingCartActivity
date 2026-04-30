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

        bool continueShopping = true;

        while (continueShopping)
        {
            Console.WriteLine("\nSTORE MENU");
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

            DisplayCart(cart, cartCount);

            Console.Write("Add another item? (YES/NO): ");
            continueShopping = Console.ReadLine().Trim().ToUpper() == "YES";
        }

        Console.WriteLine("\nCheckout complete.");
    }
}
