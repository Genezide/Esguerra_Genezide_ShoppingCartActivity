public class CartItem
{
    public Product Product;
    public int Quantity;

    public double GetSubtotal()
    {
        return Product.Price * Quantity;
    }
}
