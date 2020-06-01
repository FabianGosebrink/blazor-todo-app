using BlazorTodoApp.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Collections.Generic;

namespace BlazorTodoApp.Client.Pages.Todo
{
    public partial class TodoList : ComponentBase
    {
        [Parameter]
        public List<TodoDto> TodoModels { get; set; }

        [Parameter]
        public EventCallback<TodoDto> TodoUpdated { get; set; }

        private void ToggleDone(TodoDto todoDto)
        {
            todoDto.Done = !todoDto.Done;
            TodoUpdated.InvokeAsync(todoDto);
        }
    }
}
