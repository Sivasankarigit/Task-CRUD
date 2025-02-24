using Microsoft.EntityFrameworkCore;

using WebAPI.TaskAPI;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IPreAdvice, PreAdviceService>();

builder.Services.AddDbContext<PreAdviceDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnectionString")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
 app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
