using AutoMapper;
using CaseJuri.Application.DTOs;
using CaseJuri.Application.Interfaces;
using CaseJuri.Domain.Entities;

namespace CaseJuri.Application.UseCases.Tasks;

public class GetByIdToDoTaskUseCase
{
    private readonly IToDoTaskRepository _repo;
    private readonly IMapper _mapper;

    public GetByIdToDoTaskUseCase(IToDoTaskRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<ToDoTaskResponseDto> ExecuteAsync(Guid id)
    {
        var task = await _repo.GetByIdAsync(id)
            ?? throw new Exception("Tarefa não encontrada");

        return _mapper.Map<ToDoTaskResponseDto>(task);
    }
}
