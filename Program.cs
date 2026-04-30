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
@@ -47,9 +13,9 @@ static void Main()
CartItem[] cart = new CartItem[10];
int cartCount = 0;

        string continueShopping = "Y";
        bool continueShopping = true;

        while (continueShopping == "Y")
        while (continueShopping)
{
Console.WriteLine("\nSTORE MENU");

@@ -59,22 +25,14 @@ static void Main()
}

Console.Write("Enter product number: ");
            string productInput = Console.ReadLine();

            int productNumber;

            if (!int.TryParse(productInput, out productNumber))
            if (!int.TryParse(Console.ReadLine(), out int productNumber) ||
                productNumber < 1 || productNumber > products.Length)
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
@@ -84,11 +42,8 @@ static void Main()
}

Console.Write("Enter quantity: ");
            string quantityInput = Console.ReadLine();

            int quantity;

            if (!int.TryParse(quantityInput, out quantity) || quantity <= 0)
            if (!int.TryParse(Console.ReadLine(), out int quantity) || quantity <= 0)
{
Console.WriteLine("Invalid quantity.");
continue;
@@ -114,12 +69,6 @@ static void Main()

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
@@ -130,20 +79,27 @@ static void Main()
}

selectedProduct.DeductStock(quantity);

Console.WriteLine("Item added to cart.");

while (true)
{
                Console.Write("Add another item? (Y/N): ");
                continueShopping = Console.ReadLine().ToUpper();
                Console.Write("Add another item? (YES/NO): ");
                string input = Console.ReadLine().Trim().ToUpper();

                if (continueShopping == "Y" || continueShopping == "N")
                if (input == "YES")
{
                    continueShopping = true;
break;
}

                Console.WriteLine("Invalid input. Please enter Y or N only.");
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

@@ -154,25 +110,18 @@ static void Main()
for (int i = 0; i < cartCount; i++)
{
double subtotal = cart[i].GetSubtotal();

Console.WriteLine($"{cart[i].Product.Name} x {cart[i].Quantity} = {subtotal}");

grandTotal += subtotal;
}

Console.WriteLine($"\nGrand Total: {grandTotal}");

        double discount = 0;
        double discount = (grandTotal >= 5000) ? grandTotal * 0.10 : 0;

        if (grandTotal >= 5000)
        {
            discount = grandTotal * 0.10;
        if (discount > 0)
Console.WriteLine($"Discount (10%): {discount}");
        }

        double finalTotal = grandTotal - discount;

        Console.WriteLine($"Final Total: {finalTotal}");
        Console.WriteLine($"Final Total: {grandTotal - discount}");

Console.WriteLine("\nUpdated Stock:");
