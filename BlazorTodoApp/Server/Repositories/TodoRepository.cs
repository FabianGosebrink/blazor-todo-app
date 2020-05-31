using BlazorTodoApp.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlazorTodoApp.Server.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TodoDbContext _todoDbContext;

        public TodoRepository(TodoDbContext dbContext)
        {
            _todoDbContext = dbContext;
        }

        public TodoEntity GetSingle(Guid id)
        {
            return _todoDbContext.TodoItems.FirstOrDefault(x => x.Id == id);
        }

        public TodoEntity Add(TodoEntity item)
        {
            _todoDbContext.TodoItems.Add(item);
            return item;
        }

        public void Delete(TodoEntity item)
        {
            _todoDbContext.TodoItems.Remove(item);
        }

        public TodoEntity Update(Guid id, TodoEntity item)
        {
            _todoDbContext.TodoItems.Update(item);
            return item;
        }

        public IEnumerable<TodoEntity> GetAll(bool? done)
        {
            if (done.HasValue)
            {
                return _todoDbContext.TodoItems.Where(x => x.Done == done.Value);
            }
            return _todoDbContext.TodoItems;
        }

        public int Count()
        {
            return _todoDbContext.TodoItems.Count();
        }

        public bool Save()
        {
            return (_todoDbContext.SaveChanges() >= 0);
        }
    }
}
