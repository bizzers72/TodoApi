
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Json;
using Xunit.Abstractions;
using TodoApi.Models;

namespace ToDoApiTests
{
    public class ToDoApiTests : IDisposable
    {
        private readonly HttpClient _httpClient = new() { BaseAddress = new Uri("https://localhost:7138") };

        public void Dispose()
        {
            _httpClient.DeleteAsync("/state").GetAwaiter().GetResult();
        }

        [Fact]
        public async Task GetTaskItems()
        {
            //  Expected HTTP code
            var expectedStatusCode = System.Net.HttpStatusCode.OK;

            //  call API to get Tasks/To Do list
            HttpResponseMessage response = await _httpClient.GetAsync("/api/Tasks");

            //  Assert that the call was OK
            Assert.Equal(expectedStatusCode, response.StatusCode);

            //  Assert the current number of Task items is not null/greater than 0 (added 2 tasks manually when setting up MongoDb)
            var taskJsonString = await response.Content.ReadAsStringAsync();
            IEnumerable<TaskItem>? deserialisedTaskJson = JsonConvert.DeserializeObject<IEnumerable<TaskItem>>(taskJsonString);

            Assert.NotNull(deserialisedTaskJson);
            
        }
    }
}