using System;

class Product
{
    public int Id;
    public string Name;
    public double Price;
    public int RemainingStock;

    public void DisplayProduct()
    {
        Console.WriteLine($"{Id}. {Name} - Price: {Price} - Stock: {RemainingStock}");
    }

    public bool HasEnoughStock(int quantity)
    {
        return quantity <= RemainingStock;
    }

    public void DeductStock(int quantity)
    {
        RemainingStock -= quantity;
    }
}

class CartItem
{
    public Product Product;
    public int Quantity;

    public double GetSubtotal()
    {
        return Product.Price * Quantity;
    }
}

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

        string continueShopping = "Y";

        while (continueShopping.ToUpper() == "Y")
        {
            Console.WriteLine("\nSTORE MENU");

            foreach (Product p in products)
            {
                p.DisplayProduct();
            }

            Console.Write("Enter product number: ");
            string productInput = Console.ReadLine();

            int productNumber;

            if (!int.TryParse(productInput, out productNumber))
            {
                Console.WriteLine("Invalid product number.");
                continue;
            }

            if (productNumber < 1 || productNumber > products.Length)
            {
                Console.WriteLine("Product does not exist.");
                continue;
            }

            Product selectedProduct = products[productNumber - 1];

            if (selectedProduct.RemainingStock == 0)
            {
                Console.WriteLine("Product is out of stock.");
                continue;
            }

            Console.Write("Enter quantity: ");
            string quantityInput = Console.ReadLine();

            int quantity;

            if (!int.TryParse(quantityInput, out quantity) || quantity <= 0)
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
                if (cartCount >= cart.Length)
                {
                    Console.WriteLine("Cart is full.");
                    continue;
                }

                cart[cartCount] = new CartItem
                {
                    Product = selectedProduct,
                    Quantity = quantity
                };

                cartCount++;
            }

            selectedProduct.DeductStock(quantity);

            Console.WriteLine("Item added to cart.");

            Console.Write("Add another item? (Y/N): ");
            continueShopping = Console.ReadLine();
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

        double discount = 0;

        if (grandTotal >= 5000)
        {
            discount = grandTotal * 0.10;
            Console.WriteLine($"Discount (10%): {discount}");
        }

        double finalTotal = grandTotal - discount;

        Console.WriteLine($"Final Total: {finalTotal}");

        Console.WriteLine("\nUpdated Stock:");

        foreach (Product p in products)
        {
            Console.WriteLine($"{p.Name} - Remaining: {p.RemainingStock}");
        }

        Console.WriteLine("\nThank you for shopping!");
    }
}
