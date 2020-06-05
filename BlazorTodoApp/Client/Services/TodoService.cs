using BlazorTodoApp.Shared.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using System;

namespace BlazorTodoApp.Client.Services
{
    public class TodoService
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        };

        private HubConnection _hubConnection;
        private readonly string _todoEndpointUrl ;
        private readonly string _todoApi = "api/todos/";
        private readonly string _baseUrl = "https://localhost:5001/";

        public EventHandler<TodoDto> TodoAdded;
        public EventHandler<Guid> TodoDeleted;
        public EventHandler<TodoDto> TodoUpdated;


        public TodoService(HttpClient client)
        {
            _client = client;
            _todoEndpointUrl = $"{_baseUrl}{_todoApi}";
        }

        public async Task InitSignalR()
        {
            _hubConnection = new HubConnectionBuilder()
               .WithUrl($"{_baseUrl}todoHub")
               .Build();

            RegisterActions();

            await _hubConnection.StartAsync();
        }

        public async Task<List<TodoDto>> GetTodos()
        {
            var response = await _client.GetAsync(_todoEndpointUrl);

            response.EnsureSuccessStatusCode();

            using var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<List<TodoDto>>(responseStream, _jsonOptions);
        }

        public async Task<TodoDto> AddTodo(TodoDto createDto)
        {
            var response = await _client.PostAsJsonAsync(_todoEndpointUrl, createDto);

            response.EnsureSuccessStatusCode();

            using var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<TodoDto>(responseStream, _jsonOptions);
        }

        public async Task<TodoDto> UpdateTodo(TodoDto updateDto)
        {
            var response = await _client.PutAsJsonAsync($"{_todoEndpointUrl}{updateDto.Id}", updateDto);

            response.EnsureSuccessStatusCode();

            using var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<TodoDto>(responseStream, _jsonOptions);
        }

        private void RegisterActions()
        {
            _hubConnection.On<TodoDto>("TodoAdded", (todo) =>
            {
                TodoAdded?.Invoke(this, todo);
            });

            _hubConnection.On<TodoDto>("TodoUpdated", (todo) =>
            {
                TodoUpdated?.Invoke(this, todo);
            });

            _hubConnection.On<Guid>("TodoDeleted", (id) =>
            {
                TodoDeleted?.Invoke(this, id);
            });
        }
    }
}
