using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddDbContext<OrdersDb>(opt => opt.UseInMemoryDatabase("OrderList"));
var app = builder.Build();
app.MapControllers();

app.MapGet("/", () => "Hello World!");

app.Run();
