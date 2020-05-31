using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using BlazorTodoApp.Server.MappingProfiles;

namespace BlazorTodoApp.Server.Extensions
{
    public static class MappingExtensions
    {
        public static void AddMappingProfiles(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(TodoMappings));
        }
    }
}
