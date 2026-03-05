using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using PortfolioManager.Api.Application.Interfaces;
using PortfolioManager.Api.Common;
using PortfolioManager.Api.DTOs;

namespace PortfolioManager.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[Produces("application/json")]
public class ProjectsController : ControllerBase
{
    private readonly IProjectService _service;

    public ProjectsController(IProjectService service)
    {
        _service = service;
    }

    // =========================
    // GET PAGINADO
    // =========================
    
    [HttpGet]
    [ProducesResponseType(typeof(PagedResultDto<ProjectResponseDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<PagedResultDto<ProjectResponseDto>>> Get(
    [FromQuery] ProjectQueryParams query)
    {
        var result = await _service.GetPagedAsync(query);
        return Ok(result);
    }

    // =========================
    // GET BY ID
    // =========================
    [HttpGet("{id:long}")]
    [ProducesResponseType(typeof(ApiResponse<ProjectResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProjectResponseDto>> GetById(long id)
    {
        var project = await _service.GetByIdAsync(id);

        if (project is null)
            return NotFound(new ApiResponse<ProjectResponseDto>
            {
                Success = false,
                Errors = new List<string>
                {
                    $"Projeto {id} não encontrado."
                }
            });

        return Ok(new ApiResponse<ProjectResponseDto>
        {
            Success = true,
            Data = project
        });
    }

    // =========================
    // CREATE
    // =========================
    [HttpPost]
    [ProducesResponseType(typeof(ProjectResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ProjectResponseDto>> Create([FromBody] CreateProjectDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var id = await _service.CreateAsync(dto);
        var project = await _service.GetByIdAsync(id);

        return CreatedAtAction(
            nameof(GetById),
            new { id, version = HttpContext.GetRequestedApiVersion()?.ToString() },
            project
        );
    }

    // =========================
    // UPDATE
    // =========================
    [HttpPut("{id:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ApiResponse<ProjectResponseDto>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(long id, [FromBody] UpdateProjectDto dto)
    {
        var updated = await _service.UpdateAsync(id, dto);

        if (!updated)
            return NotFound(new ApiResponse<ProjectResponseDto>
            {
                Success = false,
                Errors = new List<string>
                {
                    $"Projeto {id} não encontrado."
                }
            });

        return NoContent();
    }

    // =========================
    // DELETE
    // =========================
    [HttpDelete("{id:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ApiResponse<ProjectResponseDto>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(long id)
    {
        var deleted = await _service.DeleteAsync(id);

        if (!deleted)
            return NotFound(new ApiResponse<ProjectResponseDto>
            {
                Success = false,
                Errors = new List<string>
                {
                    $"Projeto {id} não encontrado."
                }
            });

        return NoContent();
    }
}