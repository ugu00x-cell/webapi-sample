using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Pages;

public class TodosModel : PageModel
{
    private readonly AppDbContext _context;

    public TodosModel(AppDbContext context)
    {
        _context = context;
    }

    // Flask: todos = Todo.query.all() に相当
    public List<Todo> Todos { get; set; } = new();

    // GET /todos — ページ表示
    // Flask: @app.get("/todos") + return render_template("todos.html", todos=todos)
    public void OnGet()
    {
        Todos = _context.Todos.OrderBy(t => t.Id).ToList();
    }

    // POST /todos?handler=Add — 追加
    // Flask: @app.post("/add")
    public IActionResult OnPostAdd(string title)
    {
        if (!string.IsNullOrWhiteSpace(title))
        {
            _context.Todos.Add(new Todo { Title = title });
            _context.SaveChanges();
        }
        return RedirectToPage();  // Flask: return redirect(url_for("todos"))
    }

    // POST /todos?handler=Toggle — 完了/未完了切替
    public IActionResult OnPostToggle(int id)
    {
        var todo = _context.Todos.Find(id);
        if (todo != null)
        {
            todo.IsDone = !todo.IsDone;
            _context.SaveChanges();
        }
        return RedirectToPage();
    }

    // POST /todos?handler=Delete — 削除
    // Flask: @app.post("/delete/<int:id>")
    public IActionResult OnPostDelete(int id)
    {
        var todo = _context.Todos.Find(id);
        if (todo != null)
        {
            _context.Todos.Remove(todo);
            _context.SaveChanges();
        }
        return RedirectToPage();
    }
}
