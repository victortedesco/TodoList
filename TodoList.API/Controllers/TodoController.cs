using Microsoft.AspNetCore.Mvc;
using TodoList.Models;
using TodoList.Repositories;

namespace TodoList.Controllers
{
    [Route("api/todos")]
    [ApiController]
    public class TodoController(ITodoRepository todoRepository) : ControllerBase
    {
        private readonly ITodoRepository _todoRepository = todoRepository;

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Todo>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAll()
        {
            var todos = await _todoRepository.GetAll();
            if (!todos.Any())
                return NoContent();
            return Ok(todos);
        }

        [HttpGet]
        [Route("id/{id}")]
        [ProducesResponseType(typeof(Todo), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var todo = await _todoRepository.GetById(id);
            if (todo == null)
                return NotFound("Essa tarefa não existe.");
            return Ok(todo);
        }

        [HttpGet]
        [Route("title/{title}")]
        [ProducesResponseType(typeof(Todo), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByTitle(string title)
        {
            var todo = await _todoRepository.GetByTitle(title);
            if (todo == null)
                return NotFound("Essa tarefa não existe.");
            return Ok(todo);
        }

        [HttpGet]
        [Route("status/{isComplete}")]
        [ProducesResponseType(typeof(IEnumerable<Todo>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetByStatus(bool isComplete)
        {
            var todos = await _todoRepository.GetByStatus(isComplete);
            if (!todos.Any())
                return NoContent();
            return Ok(todos);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Todo), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add(Todo todo)
        {
            if (!await _todoRepository.Add(todo))
                return BadRequest("Erro ao adicionar tarefa.");
            return CreatedAtAction(nameof(GetById), new { id = todo.Id }, todo);
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!await _todoRepository.Delete(id))
                return NotFound("Essa tarefa não existe.");
            return Ok("Tarefa excluída com sucesso.");
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(Todo todo)
        {
            if (!await _todoRepository.Update(todo))
                return NotFound("Essa tarefa não existe.");
            return Ok("Tarefa atualizada com sucesso.");
        }
    }
}
