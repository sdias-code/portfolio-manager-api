using PortfolioManager.Api.DTOs;

namespace PortfolioManager.Api.Application.Interfaces
{
    public interface IProjectService
    {
        Task<PagedResultDto<ProjectResponseDto>> GetPagedAsync(ProjectQueryParams query);

        Task<ProjectResponseDto?> GetByIdAsync(long id, CancellationToken cancellationToken);

        Task<long> CreateAsync(CreateProjectDto dto);

        Task<bool> UpdateAsync(long id, UpdateProjectDto dto);

        Task<bool> DeleteAsync(long id);

        Task<ProjectStatsDto> GetStatsAsync();
    }
}
