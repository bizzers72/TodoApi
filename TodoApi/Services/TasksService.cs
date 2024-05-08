using TodoApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

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

}





    


