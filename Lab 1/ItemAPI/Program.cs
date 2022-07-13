using Microsoft.EntityFrameworkCore;

var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy  =>
                      {
                          policy.WithOrigins("*")
                            .AllowAnyHeader()
                            .AllowAnyMethod();;
                      });
});
builder.Services.AddControllers();
builder.Services.AddDbContext<ItemDb>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("docker_db2")));

var app = builder.Build();

app.UseCors(MyAllowSpecificOrigins);

app.MapControllers();



app.MapGet("/", () => "ItemAPI Controller!");
app.Run();