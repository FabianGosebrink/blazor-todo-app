using System;

namespace BlazorTodoApp.Server.Models
{
    public class TodoEntity
    {
        public Guid Id { get; set; }
        public string Value { get; set; }
        public bool Done { get; set; }
        public DateTime Created { get; set; }
    }
}