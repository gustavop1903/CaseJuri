using CaseJuri.Application.DTOs;
using CaseJuri.Application.UseCases.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CaseJuri.API.Controllers;

[ApiController]
[Route("api/tasks")]
public class ToDoTasksController : ControllerBase
{
    private readonly CreateToDoTaskUseCase _create;
    private readonly GetAllToDoTasksUseCase _getAll;
    private readonly GetByIdToDoTaskUseCase _getById;
    private readonly UpdateToDoTaskUseCase _update;
    private readonly DeleteToDoTaskUseCase _delete;
    private readonly StartToDoTaskUseCase _start;
    private readonly CompleteToDoTaskUseCase _complete;

    public ToDoTasksController(
        CreateToDoTaskUseCase create,
        GetAllToDoTasksUseCase getAll,
        GetByIdToDoTaskUseCase getById,
        UpdateToDoTaskUseCase update,
        DeleteToDoTaskUseCase delete,
        StartToDoTaskUseCase start,
        CompleteToDoTaskUseCase complete)
    {
        _create = create;
        _getAll = getAll;
        _getById = getById;
        _update = update;
        _delete = delete;
        _start = start;
        _complete = complete;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateToDoTaskRequest request)
    { 
        await _create.ExecuteAsync(request);
        return StatusCode(201);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _getAll.ExecuteAsync());

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
        => Ok(await _getById.ExecuteAsync(id));

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateToDoTaskRequest request)
    {
        await _update.ExecuteAsync(id, request);
        return StatusCode(204);
    }

    [HttpPost("{id:guid}/start")]
    public async Task<IActionResult> Start(Guid id)
    {    
        await _start.ExecuteAsync(id);
        return StatusCode(204);
    }   

    [HttpPost("{id:guid}/complete")]
    public async Task<IActionResult> Complete(Guid id)
    {    
        await _complete.ExecuteAsync(id);
        return StatusCode(204);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _delete.ExecuteAsync(id);
        return StatusCode(204);
    }
}
