namespace PortfolioManager.Api.DTOs;

public class ProjectQueryParams
{
    public string? Status { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}