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

class Program
{
    static void Main()
    {
        Product[] products = new Product[3];

        products[0] = new Product { Id = 1, Name = "Laptop", Price = 30000, RemainingStock = 5 };
        products[1] = new Product { Id = 2, Name = "Mouse", Price = 500, RemainingStock = 10 };
        products[2] = new Product { Id = 3, Name = "Keyboard", Price = 1200, RemainingStock = 7 };

        Console.WriteLine("STORE MENU");

        foreach (Product p in products)
        {
            p.DisplayProduct();
        }
    }
}
