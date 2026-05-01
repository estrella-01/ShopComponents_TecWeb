namespace ShopComponents.Core.CustomEntities;

public class ApiResponse<T>
{
    public T Data { get; set; }
    public Pagination? Pagination { get; set; }

    public ApiResponse(T data)
    {
        Data = data;
    }
}