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
        Product[] products = new Product[3];
        CartItem[] cart = new CartItem[10];
        int cartCount = 0;

        Console.WriteLine("Shopping Cart System");
    }
}
