namespace UserApi.Blazor.Models.Requests;

public class UserQueryParameters
{
    public int PageNumber { get; set; } = 1;

    public int PageSize { get; set; } = 10;

    public string? Search { get; set; }

    public string SortBy { get; set; } = "id";

    public bool SortDirection { get; set; }
}