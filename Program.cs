using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();

// Flask の app で直接 anthropic.Anthropic() するのと違い、
// ASP.NET Core では DI（依存性注入）でサービスを登録する
builder.Services.AddHttpClient<ClaudeService>();

// Flask の app.config["SQLALCHEMY_DATABASE_URI"] = "sqlite:///app.db" に相当
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=app.db"));

var app = builder.Build();

// Flask の db.create_all() に相当（テーブルがなければ自動作成）
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();
app.MapControllers();
app.Run();
