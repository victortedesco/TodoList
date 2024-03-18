using TodoList.Models;

namespace TodoList.Repositories
{
    public interface ITodoRepository
    {
        Task<List<Todo>> GetAll();
        Task<bool> Add(Todo todo);

        Task<Todo> GetById(Guid id);
        Task<Todo> GetByTitle(String title);
        Task<List<Todo>> GetByStatus(bool isComplete);

        Task<bool> Update(Todo todo);
        Task<bool> Delete(Guid id);
    }
}
