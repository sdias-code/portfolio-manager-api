using PortfolioManager.Api.Application.Interfaces;
using PortfolioManager.Api.DTOs;
using PortfolioManager.Api.Infrastructure.Interfaces;

public class ProjectService : IProjectService
{
    private readonly IProjectRepository _repository;
    private readonly ILogger<ProjectService> _logger;

    public ProjectService(
        IProjectRepository repository, 
        ILogger<ProjectService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<PagedResultDto<ProjectResponseDto>> GetPagedAsync(ProjectQueryParams query)
    {
        try
        {
            _logger.LogInformation(
                "Fetching projects | Status: {Status} | Page: {Page} | PageSize: {PageSize}",
                query.Status ?? "NULL",
                query.Page,
                query.PageSize
            );

            var result = await _repository.GetPagedAsync(query);

            _logger.LogInformation(
                "Fetched {Count} projects for page {Page}",
                result.Data.Count(),
                query.Page
            );

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching paged projects");

            throw;
        }
    }

    public async Task<ProjectResponseDto?> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching project with ID: {Id}", id);

        var project = await _repository.GetByIdAsync(id, cancellationToken);

        if (project == null)
        {
            _logger.LogWarning("Project with ID {Id} not found", id);
            return null;
        }

        _logger.LogInformation("Project with ID {Id} successfully fetched", id);

        return project;
    }

    public async Task<long> CreateAsync(CreateProjectDto dto)
    {
        _logger.LogInformation("Creating new project with title: {Title}", dto.Title);

        var id = await _repository.CreateAsync(dto);      

        _logger.LogInformation("Project created successfully with ID: {Id}", id);

        return id;
    }

    public async Task<bool> UpdateAsync(long id, UpdateProjectDto dto)
    {
        _logger.LogInformation("Updating project with ID: {Id}, new title: {Title}", id, dto.Title);

        var updateResult = await _repository.UpdateAsync(id, dto);

        if (!updateResult)
        {
            _logger.LogWarning("Project with ID {Id} not found for update", id);
            return false;
        }

        _logger.LogInformation("Project with ID {Id} updated successfully", id);

        return true;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        _logger.LogInformation("Deleting project with ID: {Id}", id);

        var deleteResult = await _repository.DeleteAsync(id);

        if (!deleteResult)
        {
            _logger.LogWarning("Project with ID {Id} not found for deletion", id);
            return false;
        }

        return true;
    }
}