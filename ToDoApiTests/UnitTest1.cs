
using Microsoft.AspNetCore.Mvc.Testing;
using System.Diagnostics;
using Xunit.Abstractions;

namespace ToDoApiTests
{
    public class ToDoApiTests : IDisposable
    {
        private readonly HttpClient _httpClient = new() { BaseAddress = new Uri("https://localhost:7133") };
        private const int _expectedElapsedMilliSecs = 1000;

        public void Dispose()
        {
            _httpClient.DeleteAsync("/state").GetAwaiter().GetResult();
        }

        [Fact]
        public async Task GetTaskItems()
        {
            var expectedStatusCode = System.Net.HttpStatusCode.OK;

            var stopwatch = Stopwatch.StartNew();

            var response = await _httpClient.GetAsync("/TaskItems");

            Assert.Equal(expectedStatusCode, response.StatusCode);
            Assert.True(stopwatch.ElapsedMilliseconds < _expectedElapsedMilliSecs);

            
        }
    }
}