using System.ComponentModel.DataAnnotations;

namespace PortfolioManager.Api.DTOs
{
    public class ProjectResponseDto
    {
        public long Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string? Description { get; set; }

        [Required]
        public string Status { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
