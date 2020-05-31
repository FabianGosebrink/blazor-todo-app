using BlazorTodoApp.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorTodoApp.Client.Pages.Todo
{
    public partial class TodoForm : ComponentBase
    {
        [Parameter]
        public EventCallback<TodoDto> TodoAdded { get; set; }

        private TodoDto todoModel = new TodoDto();

        private void HandleValidSubmit()
        {
            this.TodoAdded.InvokeAsync(new TodoDto() { Value = todoModel.Value });
        }
    }
}
