using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddDbContext<ItemDb>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("docker_db2")));

var app = builder.Build();
app.MapControllers();



app.MapGet("/", () => "ItemAPI Controller!");
app.Run();