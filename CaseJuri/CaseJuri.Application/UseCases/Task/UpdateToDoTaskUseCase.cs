using AutoMapper;
using CaseJuri.Application.DTOs;
using CaseJuri.Application.Interfaces;
using CaseJuri.Domain.Entities;

namespace CaseJuri.Application.UseCases.Tasks;

public class UpdateToDoTaskUseCase
{
    private readonly IToDoTaskRepository _repo;
    private readonly IMapper _mapper;

    public UpdateToDoTaskUseCase(
        IToDoTaskRepository repo,
        IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task ExecuteAsync(
        Guid id,
        UpdateToDoTaskRequest request)
    {
        var task = await _repo.GetByIdAsync(id)
            ?? throw new Exception("Tarefa não encontrada");

        task.Update(request.Titulo, request.Descricao);

        await _repo.UpdateAsync(task);
    }
}
