using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Components.DictionaryAdapter.Xml;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Moq;
using TodoApi.Controllers;
using TodoApi.Models;
using TodoApi.Services;

namespace ToDoApiTests
{
    public class UnitTests
    {
        private readonly Mock<ITasksService> _tasksService = new Mock<ITasksService>();
        private readonly TaskItem _taskItem = new() { Id = "663b3d6deabac897836596be", Name = "Test task", IsComplete = false };

        [Fact]
        public async Task GetTask_ReturnList()
        {
            //  Arrange
            _tasksService.Setup(x => x.GetAsync())
                .Returns(Task.FromResult(new List<TaskItem> { _taskItem }));
            var tasksController = new TasksController(_tasksService.Object);

            //  Act
            var result = await tasksController.Get();

            //  Assert
            Assert.Single(result);
            Assert.Equal(_taskItem, result.First());

        }

        [Fact]
        public async Task GetTask_ReturnById()
        {
            //  Arrange
            string taskId = _taskItem.Id!;
            _tasksService.Setup(x => x.GetAsync(taskId)).Returns(Task.FromResult(_taskItem));
            var tasksController = new TasksController(_tasksService.Object);

            //  Act
            var result = await tasksController.Get(taskId);

            //  Assert
            Assert.Equal(taskId, result.Value!.Id);
            Assert.Equal(_taskItem.Name, result.Value!.Name);

        }

        [Fact]
        public async Task GetTask_ReturnNotFound()
        {
            //  Arrange
            string taskId = _taskItem.Id!;
            _tasksService.Setup(x => x.GetAsync(taskId)).Returns(Task.FromResult<TaskItem>(null!));
            var tasksController = new TasksController(_tasksService.Object);

            //  Act
            var result = await tasksController.Get(taskId);


            //  Assert
            Assert.NotNull(result);
            var actualResult = result.Result as NotFoundResult;
            Assert.NotNull(actualResult);
            Assert.Equal(404, actualResult.StatusCode);

        }

        [Fact]
        public async Task PostTask_ShouldCreateOne()
        {
            //  Arrange
            _tasksService.Setup(x => x.CreateAsync(It.IsAny<TaskItem>())).Returns(Task.FromResult(_taskItem));
            var tasksController = new TasksController(_tasksService.Object);

            //  Act
            var result = await tasksController.Post(_taskItem);

            //  Assert
            Assert.NotNull(result);
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnResult = Assert.IsType<TaskItem>(createdResult.Value);
            Assert.Equal(returnResult.Name, _taskItem.Name);

        }

        [Fact]
        public async Task PutTask_ShouldUpdateOne()
        {
            //  Arrange
            string taskId = _taskItem.Id!;
            _tasksService.Setup(x => x.UpdateAsync(taskId, It.IsAny<TaskItem>())).Returns(Task.FromResult(_taskItem));
            var tasksController = new TasksController(_tasksService.Object);

            //  Act
            var result = await tasksController.Put(taskId, _taskItem);

            //  Assert
            Assert.NotNull(result);
            var updatedResult = Assert.IsType<NotFoundResult>(result);

        }

        [Fact]
        public async Task DeleteTask_ShouldDeleteOne()
        {
            //  Arrange
            string taskId = _taskItem.Id!;
            _tasksService.Setup(x => x.RemoveAsync(taskId)).Returns(Task.FromResult(_taskItem));
            var tasksController = new TasksController(_tasksService.Object);

            //  Act
            var result = await tasksController.Delete(taskId);

            //  Assert
            Assert.NotNull(result);
            var updatedResult = Assert.IsType<NotFoundResult>(result);
        }
    }
}
