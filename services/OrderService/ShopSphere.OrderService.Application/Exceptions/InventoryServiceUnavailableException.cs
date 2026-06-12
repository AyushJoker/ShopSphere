public class InventoryServiceUnavailableException
    : Exception
{
    public InventoryServiceUnavailableException()
        : base("Inventory service is unavailable.")
    {
    }
}