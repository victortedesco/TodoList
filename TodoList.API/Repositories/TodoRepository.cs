using Microsoft.EntityFrameworkCore;
using TodoList.Contexts;
using TodoList.Models;

namespace TodoList.Repositories
{
    public class TodoRepository(DataContext dataContext) : ITodoRepository
    {
        private readonly DbSet<Todo> _todos = dataContext.Todos;

        public async Task<IEnumerable<Todo>> GetAll()
        {
            return await _todos.AsNoTracking().ToListAsync();
        }

        public async Task<bool> Add(Todo todo)
        {
            if (todo == null || todo.Id == Guid.Empty) return false;
            if (todo.Title == null || todo.Title.Length < 5 || GetByTitle(todo.Title) != null) return false;
            _todos.Add(todo);
            return await dataContext.SaveChangesAsync() > 0;
        }

        public async Task<Todo> GetById(Guid id)
        {
            return await _todos.AsNoTracking().FirstOrDefaultAsync(todo => todo.Id == id);
        }

        public async Task<Todo> GetByTitle(string title)
        {
            return await _todos.AsNoTracking().FirstOrDefaultAsync(todo => todo.Title == title);
        }

        public async Task<IEnumerable<Todo>> GetByStatus(bool isComplete)
        {
            return await _todos.AsNoTracking().Where(todo => todo.IsComplete == isComplete).ToListAsync();
        }

        public async Task<bool> Update(Todo todo)
        {
            var oldTodo = await GetById(todo.Id);
            if (oldTodo == null) return false;
            _todos.Update(todo);
            return await dataContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> Delete(Guid id)
        {
            var todo = await GetById(id);
            if (todo == null) return false;
            _todos.Remove(todo);
            return await dataContext.SaveChangesAsync() > 0;
        }
    }
}
