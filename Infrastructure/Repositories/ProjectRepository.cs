using Dapper;
using PortfolioManager.Api.Data;
using PortfolioManager.Api.DTOs;
using PortfolioManager.Api.Infrastructure.Interfaces;

namespace PortfolioManager.Api.Infrastructure.Repositories;

public class ProjectRepository : IProjectRepository
{
    private readonly DapperContext _context;

    private const string BaseSelect = @"
    SELECT 
        id,
        title,
        description,
        status,
        created_at AS CreatedAt,
        updated_at AS UpdatedAt
    FROM projects";

    private const string WhereStatus =
        "WHERE (@Status IS NULL OR status = @Status)";

    public ProjectRepository(DapperContext context)
    {
        _context = context;
    }

    // =========================
    // GET PAGINADO + FILTRO
    // =========================
    public async Task<PagedResultDto<ProjectResponseDto>> GetPagedAsync(ProjectQueryParams query)
    {
        var page = query.Page <= 0 ? 1 : query.Page;
        var pageSize = query.PageSize <= 0 ? 10 : Math.Min(query.PageSize, 100);
        var offset = (page - 1) * pageSize;

        using var connection = _context.CreateConnection();

        var countSql = $@"
        SELECT COUNT(*)
        FROM projects
        {WhereStatus};";

        var totalCount = await connection.ExecuteScalarAsync<int>(countSql, new
        {
            query.Status
        });

        var dataSql = $@"
        {BaseSelect}
        {WhereStatus}
        ORDER BY id DESC
        LIMIT @PageSize OFFSET @Offset;";

        var data = await connection.QueryAsync<ProjectResponseDto>(dataSql, new
        {
            query.Status,
            PageSize = pageSize,
            Offset = offset
        });

        return new PagedResultDto<ProjectResponseDto>
        {
            Data = data.ToList(),
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize
        };
    }

    // =========================
    // GET BY ID
    // =========================
    public async Task<ProjectResponseDto?> GetByIdAsync(long id)
    {
        using var connection = _context.CreateConnection();

        var sql = $@"
        {BaseSelect}
        WHERE id = @Id;";

        return await connection.QueryFirstOrDefaultAsync<ProjectResponseDto>(sql, new { Id = id });
    }

    // =========================
    // CREATE
    // =========================
    public async Task<long> CreateAsync(CreateProjectDto dto)
    {
        const string sql = @"
        INSERT INTO projects (title, description, status)
        VALUES (@Title, @Description, @Status)
        RETURNING id;";

        using var connection = _context.CreateConnection();

        return await connection.ExecuteScalarAsync<long>(sql, dto);
    }

    // =========================
    // UPDATE
    // =========================
    public async Task<bool> UpdateAsync(long id, UpdateProjectDto dto)
    {
        const string sql = @"
        UPDATE projects
        SET title = @Title,
            description = @Description,
            status = @Status,
            updated_at = now()
        WHERE id = @Id;";

        using var connection = _context.CreateConnection();

        var rows = await connection.ExecuteAsync(sql, new
        {
            Id = id,
            dto.Title,
            dto.Description,
            dto.Status
        });

        return rows > 0;
    }

    // =========================
    // DELETE
    // =========================
    public async Task<bool> DeleteAsync(long id)
    {
        const string sql = "DELETE FROM projects WHERE id = @Id;";

        using var connection = _context.CreateConnection();

        var rows = await connection.ExecuteAsync(sql, new { Id = id });

        return rows > 0;
    }
}