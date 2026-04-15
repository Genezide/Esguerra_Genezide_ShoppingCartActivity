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
}

class Program
{
    static void Main()
    {
        Product[] products = new Product[3];

        products[0] = new Product { Id = 1, Name = "Laptop", Price = 30000, RemainingStock = 5 };
        products[1] = new Product { Id = 2, Name = "Mouse", Price = 500, RemainingStock = 10 };
        products[2] = new Product { Id = 3, Name = "Keyboard", Price = 1200, RemainingStock = 7 };

        Console.Write("Enter product number: ");
        string productInput = Console.ReadLine();

        int productNumber;

        if (!int.TryParse(productInput, out productNumber))
        {
            Console.WriteLine("Invalid product number.");
            return;
        }

        Console.Write("Enter quantity: ");
        string quantityInput = Console.ReadLine();

        int quantity;

        if (!int.TryParse(quantityInput, out quantity))
        {
            Console.WriteLine("Invalid quantity.");
            return;
        }
    }
}
