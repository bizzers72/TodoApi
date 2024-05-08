using TodoApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Microsoft.AspNetCore.Mvc;

namespace TodoApi.Services;

public class TasksService
{
    private readonly IMongoCollection<TaskItem> _tasksCollection;

    public TasksService(IOptions<TaskStoreDatabaseSettings> taskStoreDatabaseSettings)
    {
        var mongoClient = new MongoClient(taskStoreDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(taskStoreDatabaseSettings.Value.DatabaseName);

        _tasksCollection = mongoDatabase.GetCollection<TaskItem>(taskStoreDatabaseSettings.Value.TasksCollectionName);
    }

    public async Task<List<TaskItem>> GetAsync() =>
        await _tasksCollection.Find(_ => true).ToListAsync();

    public async Task<TaskItem?> GetAsync(string id) =>
        await _tasksCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(TaskItem newTaskItem) =>
        await _tasksCollection.InsertOneAsync(newTaskItem);

    public async Task UpdateAsync(string id, TaskItem updatedTaskItem) =>
        await _tasksCollection.ReplaceOneAsync(x => x.Id == id, updatedTaskItem);
}





    


