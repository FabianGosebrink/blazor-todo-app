using Microsoft.EntityFrameworkCore;
using BlazorTodoApp.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorTodoApp.Server.Repositories
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options)
           : base(options)
        {

        }

        public DbSet<TodoEntity> TodoItems { get; set; }

    }
}
