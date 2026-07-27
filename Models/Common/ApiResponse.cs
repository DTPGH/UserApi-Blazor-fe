namespace UserApi.Blazor.Models.Common;

public class ApiResponse<T>
{
    public int StatusCode { get; set; }
    public string Message { get; set; } = "";
    public T? Content { get; set; }
    public DateTime DateTime { get; set; }
}