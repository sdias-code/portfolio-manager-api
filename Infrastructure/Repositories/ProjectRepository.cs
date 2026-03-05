using Dapper;
using PortfolioManager.Api.DTOs;
using PortfolioManager.Api.Infrastructure.Interfaces;
using System.Data;

namespace PortfolioManager.Api.Infrastructure.Repositories;

public class ProjectRepository : IProjectRepository
{
    private readonly IDbConnection _connection;

    public ProjectRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    // =========================
    // GET PAGINADO + FILTRO
    // =========================
    public async Task<PagedResultDto<ProjectResponseDto>> GetPagedAsync(ProjectQueryParams query)
    {
        var page = query.Page <= 0 ? 1 : query.Page;
        var pageSize = query.PageSize <= 0 ? 10 : query.PageSize;
        var offset = (page - 1) * pageSize;

        var countSql = @"
        SELECT COUNT(*)
        FROM projects
        WHERE (@Status IS NULL OR status = @Status);";

        var totalCount = await _connection.ExecuteScalarAsync<int>(countSql, new
        {
            query.Status
        });

        var dataSql = @"
        SELECT 
            id,
            title,
            description,
            status,
            created_at AS CreatedAt,
            updated_at AS UpdatedAt
        FROM projects
        WHERE (@Status IS NULL OR status = @Status)
        ORDER BY id DESC
        LIMIT @PageSize OFFSET @Offset;";

        var data = await _connection.QueryAsync<ProjectResponseDto>(dataSql, new
        {
            query.Status,
            PageSize = pageSize,
            Offset = offset
        });

        return new PagedResultDto<ProjectResponseDto>
        {
            Data = data,
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
        var sql = @"
            SELECT 
                id,
                title,
                description,
                status,
                created_at AS CreatedAt,
                updated_at AS UpdatedAt
            FROM projects
            WHERE id = @Id;";

        return await _connection.QueryFirstOrDefaultAsync<ProjectResponseDto>(sql, new { Id = id });
    }

    // =========================
    // CREATE
    // =========================
    public async Task<long> CreateAsync(CreateProjectDto dto)
    {
        var sql = @"
            INSERT INTO projects (title, description, status)
            VALUES (@Title, @Description, @Status)
            RETURNING id;";

        return await _connection.ExecuteScalarAsync<long>(sql, dto);
    }

    // =========================
    // UPDATE
    // =========================
    public async Task<bool> UpdateAsync(long id, UpdateProjectDto dto)
    {
        var sql = @"
            UPDATE projects
            SET title = @Title,
                description = @Description,
                status = @Status,
                updated_at = now()
            WHERE id = @Id;";

        var rows = await _connection.ExecuteAsync(sql, new
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
        var sql = "DELETE FROM projects WHERE id = @Id;";
        var rows = await _connection.ExecuteAsync(sql, new { Id = id });
        return rows > 0;
    }
}