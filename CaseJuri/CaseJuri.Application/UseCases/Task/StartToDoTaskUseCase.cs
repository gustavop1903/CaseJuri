using CaseJuri.Application.Interfaces;
using CaseJuri.Domain.Entities;

namespace CaseJuri.Application.UseCases.Tasks;
public class StartToDoTaskUseCase
{
    private readonly IToDoTaskRepository _repo;

    public StartToDoTaskUseCase(IToDoTaskRepository repo)
    {
        _repo = repo;
    }

    public async Task ExecuteAsync(Guid id)
    {
        var task = await _repo.GetByIdAsync(id)
            ?? throw new Exception("Tarefa não encontrada");

        task.Start();

        await _repo.UpdateAsync(task);
    }
}
