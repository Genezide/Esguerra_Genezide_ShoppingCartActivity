using System;

class Program
{
    static void DisplayCart(CartItem[] cart, int cartCount)
    {
        Console.WriteLine("\n==================== YOUR CART ====================");

        if (cartCount == 0)
        {
            Console.WriteLine("Cart is empty.");
            return;
        }

        for (int i = 0; i < cartCount; i++)
        {
            Console.WriteLine($"{i + 1}. {cart[i].Product.Name} | Qty: {cart[i].Quantity} | Subtotal: {cart[i].GetSubtotal()}");
        }

        Console.WriteLine("===================================================");
    }

    static void Main()
    {
        Product[] products =
        {
            new Product { Id = 1, Name = "Portable SSD 1TB", Price = 6000, RemainingStock = 7 },
            new Product { Id = 2, Name = "Bluetooth Speaker", Price = 2500, RemainingStock = 9 },
            new Product { Id = 3, Name = "Smartwatch", Price = 7000, RemainingStock = 6 }
        };

        CartItem[] cart = new CartItem[10];
        int cartCount = 0;

        Order[] orders = new Order[10];
        int orderCount = 0;

        int receiptNumber = 1;

        bool running = true;

        while (running)
        {
            Console.WriteLine("\n==================== CART MENU ====================");
            Console.WriteLine("1. Add Item");
            Console.WriteLine("2. View Cart");
            Console.WriteLine("3. Remove Item");
            Console.WriteLine("4. Update Quantity");
            Console.WriteLine("5. Clear Cart");
            Console.WriteLine("6. Checkout");
            Console.WriteLine("7. View Order History");
            Console.WriteLine("===================================================");
            Console.Write("Choose an option: ");

            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.WriteLine("Invalid input.");
                continue;
            }

            if (choice == 1)
            {
                Console.WriteLine("\n==================== STORE MENU ====================");
                foreach (Product p in products)
                    p.DisplayProduct();

                Console.Write("Enter product number: ");
                if (!int.TryParse(Console.ReadLine(), out int pNum) || pNum < 1 || pNum > products.Length)
                {
                    Console.WriteLine("Invalid product.");
                    continue;
                }

                Product selected = products[pNum - 1];

                Console.Write("Enter quantity: ");
                if (!int.TryParse(Console.ReadLine(), out int qty) || qty <= 0)
                {
                    Console.WriteLine("Invalid quantity.");
                    continue;
                }

                if (!selected.HasEnoughStock(qty))
                {
                    Console.WriteLine("Not enough stock available.");
                    continue;
                }

                cart[cartCount++] = new CartItem
                {
                    Product = selected,
                    Quantity = qty
                };

                selected.DeductStock(qty);
                Console.WriteLine("Item successfully added to cart.");
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
                Console.WriteLine("Item removed successfully.");
            }
            else if (choice == 4)
            {
                DisplayCart(cart, cartCount);

                Console.Write("Enter item number: ");
                int index = int.Parse(Console.ReadLine()) - 1;

                Console.Write("Enter new quantity: ");
                int qty = int.Parse(Console.ReadLine());

                cart[index].Quantity = qty;
                Console.WriteLine("Quantity updated.");
            }
            else if (choice == 5)
            {
                cartCount = 0;
                Console.WriteLine("Cart cleared.");
            }
            else if (choice == 6)
            {
                if (cartCount == 0)
                {
                    Console.WriteLine("Cart is empty.");
                    continue;
                }

                double total = 0;
                for (int i = 0; i < cartCount; i++)
                    total += cart[i].GetSubtotal();

                double discount = (total >= 5000) ? total * 0.10 : 0;
                double finalTotal = total - discount;

                Console.WriteLine("\n==================== CHECKOUT ====================");
                Console.WriteLine($"Grand Total : {total}");
                Console.WriteLine($"Discount    : {discount}");
                Console.WriteLine($"Final Total : {finalTotal}");
                Console.WriteLine("=================================================");

                double payment;

                while (true)
                {
                    Console.Write("Enter payment amount: ");

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

                Console.WriteLine("\n==================== RECEIPT =====================");
                Console.WriteLine($"Receipt No : {receiptNumber:D4}");
                Console.WriteLine($"Date       : {DateTime.Now}");
                Console.WriteLine("-------------------------------------------------");

                for (int i = 0; i < cartCount; i++)
                {
                    Console.WriteLine($"{cart[i].Product.Name} x {cart[i].Quantity} = {cart[i].GetSubtotal()}");
                }

                Console.WriteLine("-------------------------------------------------");
                Console.WriteLine($"Grand Total : {total}");
                Console.WriteLine($"Discount    : {discount}");
                Console.WriteLine($"Final Total : {finalTotal}");
                Console.WriteLine($"Payment     : {payment}");
                Console.WriteLine($"Change      : {change}");
                Console.WriteLine("=================================================");

                orders[orderCount++] = new Order
                {
                    ReceiptNumber = receiptNumber,
                    FinalTotal = finalTotal
                };

                receiptNumber++;

                Console.WriteLine("\nLOW STOCK ALERT:");
                foreach (Product p in products)
                {
                    if (p.RemainingStock <= 5)
                    {
                        Console.WriteLine($"- {p.Name} has only {p.RemainingStock} left.");
                    }
                }

                cartCount = 0;
            }
            else if (choice == 7)
            {
                Console.WriteLine("\n==================== ORDER HISTORY ====================");

                if (orderCount == 0)
                {
                    Console.WriteLine("No orders yet.");
                }
                else
                {
                    for (int i = 0; i < orderCount; i++)
                    {
                        Console.WriteLine($"Receipt #{orders[i].ReceiptNumber:D4} | Final Total: {orders[i].FinalTotal}");
                    }
                }

                Console.WriteLine("=======================================================");
            }
        }

        Console.WriteLine("\nThank you for shopping!");
    }
}
