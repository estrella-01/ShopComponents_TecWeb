namespace ShopComponents_TecWeb.Api.Responses;

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public T? Data { get; set; }
    public string Message { get; set; } = string.Empty;

    public ApiResponse()
    {
    }

    public ApiResponse(T? data, string message)
    {
        Success = true;
        Data = data;
        Message = message;
    }
}