namespace ShopSphere.OrderService.Application.Interfaces;

public interface IInventoryServiceClient
{
    Task ReserveStockAsync(
        Guid productId,
        int quantity);

    Task ReleaseStockAsync(
        Guid productId,
        int quantity);
    Task DeductStockAsync(
    Guid productId,
    int quantity);
}