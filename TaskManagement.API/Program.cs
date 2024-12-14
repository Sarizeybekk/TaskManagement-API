using TaskManagement.Data.Context;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Business.Services;
using TaskManagement.Domain.Interface.Services;
using FluentValidation;
using TaskManagement.API.Validation;
using TaskManagement.Business.Validators;
using TaskManagement.Domain.Entities;
using Task = TaskManagement.Domain.Entities.Task;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("InMemoryDb"));

// Add FluentValidation to Validation Layer
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IValidator<User>, UserValidator>();

builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<IValidator<Task>, TaskValidator>();

builder.Services.AddControllers();

// Add Swagger for API testing.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();