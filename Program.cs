using Microsoft.EntityFrameworkCore;
using UserBackend.Data;
using UserBackend.Helpers;
using UserBackend.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
builder.Services.AddDbContext<UserDbContext>(options =>
{

    options.UseSqlServer(builder.Configuration.GetConnectionString("DevConnection"));
});

builder.Services.AddControllers();
builder.Services.AddScoped<IUserRepository, Userrepository>();
builder.Services.AddScoped<JwtService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors(options => options
.WithOrigins(new[] {"http://localhost:3000"} )
.AllowAnyHeader()
.AllowAnyMethod()
.AllowCredentials()
);

app.UseAuthorization();

app.MapControllers();

app.Run();
