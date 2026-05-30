namespace ShopComponents.Core.CustomEntities;

public class ApiResponse<T>
{
    public T Data { get; set; }
    public Pagination? Pagination { get; set; }
    public Message[]? Messages { get; set; }   // ← AGREGAR esta línea

    public ApiResponse(T data)
    {
        Data = data;
    }
}