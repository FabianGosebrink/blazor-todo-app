using Blazored.Toast.Services;
using BlazorTodoApp.Client.Services;
using BlazorTodoApp.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorTodoApp.Client.Pages.Todo
{
    public partial class Todo : ComponentBase
    {
        [Inject]
        private TodoService todoService { get; set; }

        [Inject]
        private IToastService toastService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            todoModels = await todoService.GetTodos();
            todoService.TodoAdded += HandleTodoAdded;
            todoService.TodoUpdated += HandleTodoUpdated;
            await todoService.InitSignalR();
        }

        public List<TodoDto> todoModels = new List<TodoDto>();

        private async Task AddTodo(TodoDto todoModel)
        {
            await todoService.AddTodo(todoModel);
        }

        private async Task UpdateTodo(TodoDto todoModel)
        {
            await todoService.UpdateTodo(todoModel);
        }

        private async void HandleTodoAdded(object sender, TodoDto args)
        {
            toastService.ShowInfo("TodoItem added");

            todoModels = await todoService.GetTodos();

            OrderDoneItemsToBottom();
            StateHasChanged();
        }

        private void HandleTodoUpdated(object sender, TodoDto args)
        {
            var existingTodo = todoModels.FirstOrDefault(x => x.Id == args.Id);
            
            var index = todoModels.IndexOf(existingTodo);
            todoModels[index] = args;

            toastService.ShowInfo("TodoItem updated");

            OrderDoneItemsToBottom();
            StateHasChanged();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);

            if (todoService.TodoAdded != null)
            {
                todoService.TodoAdded -= HandleTodoAdded;
            }
        }

        private void OrderDoneItemsToBottom()
        {
            todoModels = todoModels.OrderBy(x => x.Done).ToList();
        }
    }
}
