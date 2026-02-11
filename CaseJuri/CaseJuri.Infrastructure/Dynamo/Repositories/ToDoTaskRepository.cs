using Amazon.DynamoDBv2.DataModel;
using CaseJuri.Application.Interfaces;
using CaseJuri.Domain.Entities;
using CaseJuri.Infrastructure.Dynamo.Schema;
using CaseJuri.Domain.Enums;

namespace CaseJuri.Infrastructure.Dynamo;

public class ToDoTaskRepository : IToDoTaskRepository
{
    private readonly DynamoDBContext _context;

    public ToDoTaskRepository(DynamoContext dynamoContext)
    {
        _context = dynamoContext.Context;
    }


    public async Task CreateAsync(ToDoTask task)
    {
        var item = ToItem(task);
        await _context.SaveAsync(item);
    }

    public async Task UpdateAsync(ToDoTask task)
    {
        var item = ToItem(task);
        await _context.SaveAsync(item);
    }

    public async Task<ToDoTask?> GetByIdAsync(Guid id)
    {
        var item = await _context.LoadAsync<ToDoTaskItem>(id.ToString());

        if (item == null)
            return null;

        return ToDomain(item);
    }

    public async Task<IEnumerable<ToDoTask>> GetAllAsync()
    {
        var search = _context.ScanAsync<ToDoTaskItem>(new List<ScanCondition>());
        var items = await search.GetRemainingAsync();

        return items.Select(ToDomain);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _context.DeleteAsync<ToDoTaskItem>(id.ToString());
    }

    private static ToDoTaskItem ToItem(ToDoTask task)
    {
        return new ToDoTaskItem
        {
            Id = task.Id.ToString(),
            Titulo = task.Titulo,
            Descricao = task.Descricao,
            Status = task.Status.ToString(),
            CriadoPor = task.CriadoPor,
            DataCriacao = task.DataCriacao,
            DataConclusao = task.DataConclusao
        };
    }

    private static ToDoTask ToDomain(ToDoTaskItem item)
    {
        var task = new ToDoTask(
            item.Titulo,
            item.Descricao,
            item.CriadoPor
        );

        typeof(ToDoTask).GetProperty(nameof(ToDoTask.Id))!
            .SetValue(task, Guid.Parse(item.Id));

        typeof(ToDoTask).GetProperty(nameof(ToDoTask.Status))!
            .SetValue(task, Enum.Parse<StatusTask>(item.Status));

        typeof(ToDoTask).GetProperty(nameof(ToDoTask.DataCriacao))!
            .SetValue(task, item.DataCriacao);

        typeof(ToDoTask).GetProperty(nameof(ToDoTask.DataConclusao))!
            .SetValue(task, item.DataConclusao);

        return task;
    }
}
