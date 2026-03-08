namespace PortfolioManager.Api.DTOs;

public class ProjectQueryParams
{
    public string? Status { get; set; }

    public string? Search { get; set; }

    public string? SortBy { get; set; } = "id";

    public string? Order { get; set; } = "desc";

    public int Page { get; set; } = 1;

    public int PageSize { get; set; } = 10;
}