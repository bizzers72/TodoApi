using TodoApi.Models;

namespace TodoApi.Services
{
    public interface ITasksService
    {
        Task<List<TaskItem>> GetAsync();

        Task<TaskItem> GetAsync(string id);

        Task CreateAsync(TaskItem newTaskItem);

        Task UpdateAsync(string id, TaskItem updatedTaskItem);

        Task RemoveAsync(string id);

    }
}
