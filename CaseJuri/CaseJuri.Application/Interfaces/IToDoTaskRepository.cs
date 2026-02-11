using CaseJuri.Domain.Entities;

namespace CaseJuri.Application.Interfaces
{

    public interface IToDoTaskRepository
    {
        Task CreateAsync(ToDoTask task);
        Task<ToDoTask?> GetByIdAsync(Guid id);
        Task<IEnumerable<ToDoTask>> GetAllAsync();
        Task UpdateAsync(ToDoTask task);
        Task DeleteAsync(Guid id);
        // Task CompleteAsync(Guid id);
        // Task StartAsync(Guid id);
    }
}