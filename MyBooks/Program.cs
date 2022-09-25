using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using MyBooks.Data;
using MyBooks.Data.Services;
using MyBooks.Exceptions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Configure DbContext 
builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("MyBooksDb"));

// Configure Services
builder.Services.AddTransient<BookService>();

// Versioning
builder.Services.AddApiVersioning(config =>
{
    config.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    config.AssumeDefaultVersionWhenUnspecified = true;

    config.ApiVersionReader = new HeaderApiVersionReader("custom-version-header");
});    

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

//app.ConfigureBuildInExceptionHandler();
app.ConfigureCustomExceptionHandler();

app.MapControllers();

AppDbInitializer.Seed(app);

app.Run();
