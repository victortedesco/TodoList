using Microsoft.AspNetCore.Mvc;
using Moq;
using TodoList.Controllers;
using TodoList.Models;
using TodoList.Repositories;

namespace TodoList.Testes.Controllers
{
    public class TodoControllerTest
    {
        private readonly Mock<ITodoRepository> _repository;
        private readonly TodoController _controller;

        public TodoControllerTest()
        {
            _repository = new Mock<ITodoRepository>();
            _controller = new TodoController(_repository.Object);
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

            var resultado = await _controller.GetAll();

            var okResult = Assert.IsType<OkObjectResult>(resultado);
            var model = Assert.IsAssignableFrom<List<Todo>>(okResult.Value);
            Assert.Equal(tarefas, model);
        }

        [Fact]
        public async Task GetById_ReturnsOk()
        {
            var id = Guid.NewGuid();
            var tarefa = new Todo { Id = id, Title = "Tarefa 1", IsComplete = false };
            _repository.Setup(r => r.GetById(id)).ReturnsAsync(tarefa);

            var resultado = await _controller.GetById(id);

            var okResult = Assert.IsType<OkObjectResult>(resultado);
            var model = Assert.IsAssignableFrom<Todo>(okResult.Value);
            Assert.Equal(tarefa, model);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound()
        {
            var id = Guid.NewGuid();
            _repository.Setup(r => r.GetById(id)).ReturnsAsync((Todo)null);

            var resultado = await _controller.GetById(id);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(resultado);
            Assert.Equal("Essa tarefa não existe.", notFoundResult.Value);
        }

        [Fact]
        public async Task GetByTitle_ReturnsOk()
        {
            var titulo = "Tarefa 1";
            var tarefa = new Todo { Id = Guid.NewGuid(), Title = titulo, IsComplete = false };
            _repository.Setup(r => r.GetByTitle(titulo)).ReturnsAsync(tarefa);

            var resultado = await _controller.GetByTitle(titulo);

            var okResult = Assert.IsType<OkObjectResult>(resultado);
            var model = Assert.IsAssignableFrom<Todo>(okResult.Value);
            Assert.Equal(tarefa, model);
        }

        [Fact]
        public async Task GetByTitle_ReturnsNotFound()
        {
            var titulo = "Tarefa 1";
            _repository.Setup(r => r.GetByTitle(titulo)).ReturnsAsync((Todo)null);

            var resultado = await _controller.GetByTitle(titulo);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(resultado);
            Assert.Equal("Essa tarefa não existe.", notFoundResult.Value);
        }

        [Fact]
        public async Task GetByStatus_ReturnsTodos()
        {
            var tarefas = new List<Todo>
            {
                new() { Id = Guid.NewGuid(), Title = "Tarefa 1", IsComplete = false },
                new() { Id = Guid.NewGuid(), Title = "Tarefa 2", IsComplete = true }
            };
            _repository.Setup(r => r.GetByStatus(false)).ReturnsAsync(tarefas);

            var resultado = await _controller.GetByStatus(false);

            var okResult = Assert.IsType<OkObjectResult>(resultado);
            var model = Assert.IsAssignableFrom<List<Todo>>(okResult.Value);
            Assert.Equal(tarefas, model);
        }

        [Fact]
        public async Task Add_ReturnsCreatedAtAction()
        {
            var tarefa = new Todo { Id = Guid.NewGuid(), Title = "Tarefa 1", IsComplete = false };
            _repository.Setup(r => r.Add(tarefa)).ReturnsAsync(true);

            var resultado = await _controller.Add(tarefa);

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(resultado);
            var model = Assert.IsAssignableFrom<Todo>(createdAtActionResult.Value);
            Assert.Equal(tarefa, model);
        }

        [Fact]
        public async Task Add_ReturnsBadRequest()
        {
            var tarefa = new Todo { Id = Guid.NewGuid(), Title = "Tarefa 1", IsComplete = false };
            _repository.Setup(r => r.Add(tarefa)).ReturnsAsync(false);

            var resultado = await _controller.Add(tarefa);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(resultado);
            Assert.Equal("Erro ao adicionar tarefa.", badRequestResult.Value);
        }

        [Fact]
        public async Task Delete_ReturnsOk()
        {
            var id = Guid.NewGuid();
            _repository.Setup(r => r.Delete(id)).ReturnsAsync(true);

            var resultado = await _controller.Delete(id);

            var okResult = Assert.IsType<OkObjectResult>(resultado);
            Assert.Equal("Tarefa excluída com sucesso.", okResult.Value);
        }

        [Fact]
        public async Task DeleteReturnsNotFound()
        {
            var id = Guid.NewGuid();
            _repository.Setup(r => r.Delete(id)).ReturnsAsync(false);

            var resultado = await _controller.Delete(id);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(resultado);
            Assert.Equal("Essa tarefa não existe.", notFoundResult.Value);
        }
    }
}
