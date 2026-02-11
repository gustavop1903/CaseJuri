using CaseJuri.Domain.Entities;
using CaseJuri.Application.Interfaces;

namespace CaseJuri.Application.UseCases.Tasks;

public class DeleteToDoTaskUseCase
{
    private readonly IToDoTaskRepository _repo;

    public DeleteToDoTaskUseCase(IToDoTaskRepository repo)
    {
        _repo = repo;
    }

    public async Task ExecuteAsync(Guid id)
    {
        await _repo.DeleteAsync(id);
    }
}
