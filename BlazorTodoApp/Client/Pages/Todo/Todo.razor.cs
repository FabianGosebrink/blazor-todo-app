using BlazorTodoApp.Client.Services;
using BlazorTodoApp.Shared.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorTodoApp.Client.Pages.Todo
{
    public partial class Todo : ComponentBase
    {
        [Inject]
        private TodoService todoService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            todoModels = await todoService.GetTodos();
        }

        public List<TodoDto> todoModels = new List<TodoDto>();

        private async Task AddTodo(TodoDto todoModel)
        {
            var newModel = await todoService.AddTodo(todoModel);
            todoModels.Insert(0, newModel);
        }

        private async Task UpdateTodo(TodoDto todoModel)
        {
            var updatedModel = await todoService.UpdateTodo(todoModel);
            var existingModel = todoModels.First(x => x.Id == updatedModel.Id);
            var index = todoModels.IndexOf(existingModel);

            if (index != -1)
            {
                todoModels[index] = updatedModel;
            }

            todoModels = todoModels.OrderBy(x => x.Done).ToList();
        }
    }
}
