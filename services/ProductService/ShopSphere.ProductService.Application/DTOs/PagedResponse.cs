namespace ShopSphere.ProductService.Application.DTOs;

public class PagedResponse<T>
{
    public List<T> Items { get; set; } = [];

    public int PageNumber { get; set; }

    public int PageSize { get; set; }

    public int TotalCount { get; set; }
}