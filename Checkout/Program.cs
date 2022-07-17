using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
//builder.Services.AddDbContext<OrdersDb>(opt => opt.UseInMemoryDatabase("OrderList"));
builder.Services.AddDbContext<OrdersDb>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("Orders_db2")));
var app = builder.Build();
app.MapControllers();

app.MapGet("/", () => "Checkout Controller");

app.Run();
