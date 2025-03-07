using Microsoft.EntityFrameworkCore;
using MyWebApi.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. Configure EF Core to use SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. Add controllers and other services
builder.Services.AddControllers();

var app = builder.Build();

// 3. Map controllers
app.MapControllers();

app.Run();
