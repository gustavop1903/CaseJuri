using CaseJuri.Application.Interfaces;
using CaseJuri.Domain.Entities;
using MongoDB.Driver;

namespace CaseJuri.Infrastructure.Mongo;

public class MongoToDoTaskRepository : IToDoTaskRepository
{
    private readonly IMongoCollection<ToDoTask> _collection;

    public MongoToDoTaskRepository(IMongoDatabase database)
    {
        _collection = database.GetCollection<ToDoTask>("ToDoTasks");
    }

    public async Task<IEnumerable<ToDoTask>> GetAllAsync()
    {
        return await _collection.Find(_ => true).ToListAsync();
    }

    public async Task<ToDoTask?> GetByIdAsync(Guid id)
    {
        return await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task CreateAsync(ToDoTask task)
    {
        await _collection.InsertOneAsync(task);
    }

    public async Task UpdateAsync(ToDoTask task)
    {
        await _collection.ReplaceOneAsync(x => x.Id == task.Id, task);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _collection.DeleteOneAsync(x => x.Id == id);
    }
}
