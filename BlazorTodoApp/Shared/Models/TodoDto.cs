using System;

namespace BlazorTodoApp.Shared.Models
{
    public class TodoDto
    {
        public Guid Id { get; set; }
        public string Value { get; set; }
        public bool Done { get; set; }
        public DateTime Created { get; set; }
    }
}
