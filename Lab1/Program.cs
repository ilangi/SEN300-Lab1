using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddDbContext<ItemDb>(opt => opt.UseInMemoryDatabase("Catalog"));
var app = builder.Build();
app.MapControllers();



app.MapGet("/", () => "Hello World!");
app.Run();