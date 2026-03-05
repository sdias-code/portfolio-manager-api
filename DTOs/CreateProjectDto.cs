using System.ComponentModel.DataAnnotations;

namespace PortfolioManager.Api.DTOs;

public class CreateProjectDto
{
    [Required]
    [MaxLength(150)]
    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    [Required]
    public string Status { get; set; } = string.Empty;
}