using BlazorTodoApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorTodoApp.Client.Pages.Todo
{
    public class TodoClient
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        };
        private readonly string todoEndpointUrl = "api/todos/";

        public TodoClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<TodoDto>> GetTodos()
        {
            var response = await _client.GetAsync(todoEndpointUrl);

            response.EnsureSuccessStatusCode();

            using var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<List<TodoDto>>(responseStream);
        }

        public async Task<TodoDto> AddTodo(TodoDto createDto)
        {
            var response = await _client.PostAsJsonAsync(todoEndpointUrl, createDto);

            response.EnsureSuccessStatusCode();

            using var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<TodoDto>(responseStream, _jsonOptions);
        }

        public async Task<TodoDto> UpdateTodo(TodoDto updateDto)
        {
            var response = await _client.PutAsJsonAsync($"{todoEndpointUrl}{updateDto.Id}", updateDto);

            response.EnsureSuccessStatusCode();

            using var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<TodoDto>(responseStream, _jsonOptions);
        }
    }
}
