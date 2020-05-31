using BlazorTodoApp.Server.Models;
using AutoMapper;
using BlazorTodoApp.Shared.Models;

namespace BlazorTodoApp.Server.MappingProfiles
{
    public class TodoMappings : Profile
    {
        public TodoMappings()
        {
            CreateMap<TodoEntity, TodoDto>().ReverseMap();
            CreateMap<TodoEntity, TodoCreateDto>().ReverseMap();
            CreateMap<TodoEntity, TodoUpdateDto>().ReverseMap();
        }
    }
}
