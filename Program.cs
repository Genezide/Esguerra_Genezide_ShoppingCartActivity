using System;

class Program
{
    static void Main()
    {
        Product[] products = new Product[3];

        products[0] = new Product { Id = 1, Name = "Laptop", Price = 30000, RemainingStock = 5 };
        products[1] = new Product { Id = 2, Name = "Mouse", Price = 500, RemainingStock = 10 };
        products[2] = new Product { Id = 3, Name = "Keyboard", Price = 1200, RemainingStock = 7 };

        CartItem[] cart = new CartItem[10];
        int cartCount = 0;

        bool continueShopping = true;

        while (continueShopping)
        {
            Console.WriteLine("\nSTORE MENU");

            foreach (Product p in products)
            {
                p.DisplayProduct();
            }

            Console.Write("Enter product number: ");

            if (!int.TryParse(Console.ReadLine(), out int productNumber) ||
                productNumber < 1 || productNumber > products.Length)
            {
                Console.WriteLine("Invalid product number.");
                continue;
            }

            Product selectedProduct = products[productNumber - 1];

            if (selectedProduct.RemainingStock == 0)
            {
                Console.WriteLine("Product is out of stock.");
                continue;
            }

            Console.Write("Enter quantity: ");

            if (!int.TryParse(Console.ReadLine(), out int quantity) || quantity <= 0)
            {
                Console.WriteLine("Invalid quantity.");
                continue;
            }

            if (!selectedProduct.HasEnoughStock(quantity))
            {
                Console.WriteLine("Not enough stock available.");
                continue;
            }

            bool duplicate = false;

            for (int i = 0; i < cartCount; i++)
            {
                if (cart[i].Product.Id == selectedProduct.Id)
                {
                    cart[i].Quantity += quantity;
                    duplicate = true;
                    break;
                }
            }

            if (!duplicate)
            {
                cart[cartCount] = new CartItem
                {
                    Product = selectedProduct,
                    Quantity = quantity
                };

                cartCount++;
            }

            selectedProduct.DeductStock(quantity);
            Console.WriteLine("Item added to cart.");

            while (true)
            {
                Console.Write("Add another item? (YES/NO): ");
                string input = Console.ReadLine().Trim().ToUpper();

                if (input == "YES")
                {
                    continueShopping = true;
                    break;
                }
                else if (input == "NO")
                {
                    continueShopping = false;
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter YES or NO only.");
                }
            }
        }

        Console.WriteLine("\n===== RECEIPT =====");

        double grandTotal = 0;

        for (int i = 0; i < cartCount; i++)
        {
            double subtotal = cart[i].GetSubtotal();
            Console.WriteLine($"{cart[i].Product.Name} x {cart[i].Quantity} = {subtotal}");
            grandTotal += subtotal;
        }

        Console.WriteLine($"\nGrand Total: {grandTotal}");

        double discount = (grandTotal >= 5000) ? grandTotal * 0.10 : 0;

        if (discount > 0)
            Console.WriteLine($"Discount (10%): {discount}");

        Console.WriteLine($"Final Total: {grandTotal - discount}");

        Console.WriteLine("\nUpdated Stock:");

        foreach (Product p in products)
        {
            Console.WriteLine($"{p.Name} - Remaining: {p.RemainingStock}");
        }

        Console.WriteLine("\nThank you for shopping!");
    }
}
