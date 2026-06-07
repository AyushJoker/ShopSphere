public class ProductNotFoundException
    : Exception
{
    public ProductNotFoundException(Guid productId): base($"Product '{productId}' not found.")
    {
    }
}