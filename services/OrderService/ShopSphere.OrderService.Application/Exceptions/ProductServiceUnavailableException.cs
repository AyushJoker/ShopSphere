public class ProductServiceUnavailableException: Exception
{
    public ProductServiceUnavailableException()
        : base("Product service is currently unavailable.")
    {
    }
}