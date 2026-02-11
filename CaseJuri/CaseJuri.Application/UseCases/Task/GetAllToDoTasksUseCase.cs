using AutoMapper;
using CaseJuri.Application.DTOs;
using CaseJuri.Application.Interfaces;
using CaseJuri.Domain.Entities;

namespace CaseJuri.Application.UseCases.Tasks;

public class GetAllToDoTasksUseCase
{
    private readonly IToDoTaskRepository _repo;
    private readonly IMapper _mapper;

    public GetAllToDoTasksUseCase(IToDoTaskRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<List<ToDoTaskResponseDto>> ExecuteAsync()
    {
        var tasks = await _repo.GetAllAsync();
        return _mapper.Map<List<ToDoTaskResponseDto>>(tasks);
    }
}
