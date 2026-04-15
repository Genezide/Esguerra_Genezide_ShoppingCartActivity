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
}

class CartItem
{
    public Product Product;
    public int Quantity;
}

class Program
{
    static void Main()
    {
        CartItem[] cart = new CartItem[10];
        int cartCount = 0;

        Console.WriteLine("RECEIPT");

        double grandTotal = 0;

        for (int i = 0; i < cartCount; i++)
        {
            double subtotal = cart[i].Product.Price * cart[i].Quantity;
            Console.WriteLine($"{cart[i].Product.Name} x {cart[i].Quantity} = {subtotal}");
            grandTotal += subtotal;
        }

        Console.WriteLine($"Grand Total: {grandTotal}");
    }
}
