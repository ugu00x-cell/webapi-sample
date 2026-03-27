using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodosController : ControllerBase
{
    // Flask の db.session に相当（DIで注入される）
    private readonly AppDbContext _context;

    public TodosController(AppDbContext context)
    {
        _context = context;
    }

    // GET /api/todos
    // Flask: Todo.query.all()
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_context.Todos.ToList());
    }

    // GET /api/todos/1
    // Flask: Todo.query.get_or_404(id)
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var todo = _context.Todos.Find(id);
        if (todo == null) return NotFound();
        return Ok(todo);
    }

    // POST /api/todos
    // Flask: db.session.add(todo) + db.session.commit()
    [HttpPost]
    public IActionResult Create([FromBody] Todo todo)
    {
        _context.Todos.Add(todo);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetById), new { id = todo.Id }, todo);
    }

    // PUT /api/todos/1
    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] Todo input)
    {
        var todo = _context.Todos.Find(id);
        if (todo == null) return NotFound();

        todo.Title = input.Title;
        todo.IsDone = input.IsDone;
        _context.SaveChanges();
        return Ok(todo);
    }

    // DELETE /api/todos/1
    // Flask: db.session.delete(todo) + db.session.commit()
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var todo = _context.Todos.Find(id);
        if (todo == null) return NotFound();

        _context.Todos.Remove(todo);
        _context.SaveChanges();
        return NoContent();
    }
}
