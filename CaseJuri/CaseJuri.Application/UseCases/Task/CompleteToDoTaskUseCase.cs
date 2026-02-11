using CaseJuri.Application.Interfaces;
using CaseJuri.Domain.Entities;

namespace CaseJuri.Application.UseCases.Tasks;

public class CompleteToDoTaskUseCase
{
    private readonly IToDoTaskRepository _repo;

    public CompleteToDoTaskUseCase(IToDoTaskRepository repo)
    {
        _repo = repo;
    }

    public async Task ExecuteAsync(Guid id)
    {
        var task = await _repo.GetByIdAsync(id)
            ?? throw new Exception("Tarefa não encontrada");

        task.Finish();

        await _repo.UpdateAsync(task);
    }
}
