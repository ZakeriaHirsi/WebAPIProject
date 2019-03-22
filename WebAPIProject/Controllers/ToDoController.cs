using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIProject.Models;

namespace WebAPIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly ToDoContext __Context;

        public ToDoController(ToDoContext context)
        {
            __Context = context;

            if (__Context.ToDoItems.Count() == 0)
            {
                __Context.ToDoItems.Add(new TodoItem { Name = "Item 1" });
                __Context.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetToDoItems()
        {
            return await __Context.ToDoItems.ToListAsync();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TodoItem), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TodoItem>> GetToDoItem(int id)
        {
            TodoItem _ToDoItem = await __Context.ToDoItems.FindAsync(id);

            if (_ToDoItem == null)
            {
                NotFound();
            }

            return _ToDoItem;
        }

        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostToDoItem(TodoItem item)
        {
            __Context.ToDoItems.Add(item);
            await __Context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetToDoItem), new { id = item.ID }, item);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutToDoItem(long id, TodoItem item)
        {
            if (id != item.ID)
            {
                return BadRequest();
            }
            __Context.Entry(item).State = EntityState.Modified;
            await __Context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteToDoItem(int id)
        {
            TodoItem _item = await __Context.ToDoItems.FindAsync(id);

            if (_item == null)
            {
                return NotFound();
            }

            __Context.ToDoItems.Remove(_item);
            await __Context.SaveChangesAsync();

            return NoContent();
        }
    }
}