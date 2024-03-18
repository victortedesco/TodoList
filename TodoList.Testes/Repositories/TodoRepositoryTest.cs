using Moq;
using TodoList.Models;
using TodoList.Repositories;

namespace TodoList.Tests.Repositories
{
    public class TodoRepositoryTest
    {
        private readonly Mock<ITodoRepository> _repository;

        public TodoRepositoryTest()
        {
            _repository = new Mock<ITodoRepository>();
        }

        [Fact]
        public async Task GetAll_ReturnsTodos()
        {
            var tarefas = new List<Todo>
            {
                new() { Id = Guid.NewGuid(), Title = "Tarefa 1", IsComplete = false },
                new() { Id = Guid.NewGuid(), Title = "Tarefa 2", IsComplete = true }
            };
            _repository.Setup(r => r.GetAll()).ReturnsAsync(tarefas);

            var resultado = await _repository.Object.GetAll();

            Assert.Equal(tarefas, resultado);
        }

        [Fact]
        public async Task GetById_ReturnsTodo()
        {
            var id = Guid.NewGuid();
            var tarefa = new Todo { Id = id, Title = "Tarefa 1", IsComplete = false };
            _repository.Setup(r => r.GetById(id)).ReturnsAsync(tarefa);

            var resultado = await _repository.Object.GetById(id);

            Assert.Equal(tarefa, resultado);
        }

        [Fact]
        public async Task GetById_ReturnsNull()
        {
            var id = Guid.NewGuid();
            _repository.Setup(r => r.GetById(id)).ReturnsAsync((Todo)null);

            var resultado = await _repository.Object.GetById(id);

            Assert.Null(resultado);
        }

        [Fact]
        public async Task GetByTitle_ReturnsTodo()
        {
            var title = "Tarefa 1";
            var tarefa = new Todo { Id = Guid.NewGuid(), Title = title, IsComplete = false };
            _repository.Setup(r => r.GetByTitle(title)).ReturnsAsync(tarefa);

            var resultado = await _repository.Object.GetByTitle(title);

            Assert.Equal(tarefa, resultado);
        }

        [Fact]
        public async Task GetByTitle_ReturnsNull()
        {
            var title = "Tarefa 1";
            _repository.Setup(r => r.GetByTitle(title)).ReturnsAsync((Todo)null);

            var resultado = await _repository.Object.GetByTitle(title);

            Assert.Null(resultado);
        }

        [Fact]
        public async Task GetByStatus_ReturnsTodos()
        {
            var tarefas = new List<Todo>
            {
                new() { Id = Guid.NewGuid(), Title = "Tarefa 1", IsComplete = false },
                new() { Id = Guid.NewGuid(), Title = "Tarefa 2", IsComplete = false }
            };
            _repository.Setup(r => r.GetByStatus(false)).ReturnsAsync(tarefas);

            var resultado = await _repository.Object.GetByStatus(false);

            Assert.Equal(tarefas, resultado);
        }

        [Fact]
        public async Task Update_ReturnsTrue()
        {
            var id = Guid.NewGuid();
            var tarefa = new Todo { Id = id, Title = "Tarefa 1", IsComplete = false };
            _repository.Setup(r => r.Update(tarefa)).ReturnsAsync(true);

            var resultado = await _repository.Object.Update(tarefa);

            Assert.True(resultado);
        }

        [Fact]
        public async Task Update_ReturnsFalse()
        {
            var id = Guid.NewGuid();
            var tarefa = new Todo { Id = id, Title = "Tarefa 1", IsComplete = false };
            _repository.Setup(r => r.Update(tarefa)).ReturnsAsync(false);

            var resultado = await _repository.Object.Update(tarefa);

            Assert.False(resultado);
        }

        [Fact]
        public async Task Delete_ReturnsTrue()
        {
            var id = Guid.NewGuid();
            _repository.Setup(r => r.Delete(id)).ReturnsAsync(true);

            var resultado = await _repository.Object.Delete(id);

            Assert.True(resultado);
        }

        [Fact]
        public async Task Delete_ReturnsFalse()
        {
            var id = Guid.NewGuid();
            _repository.Setup(r => r.Delete(id)).ReturnsAsync(false);

            var resultado = await _repository.Object.Delete(id);

            Assert.False(resultado);
        }
    }
}
