using FootballApp;
using FootballApp.Repositories;
using FootballApp.Validators;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using FluentValidation.AspNetCore;
using FootballApp.Entities;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IUserRepository, UserRepository>(); 

builder.Services.AddControllers();

builder.Services.AddDbContext<FootballDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddValidatorsFromAssemblyContaining<UserRegisterValidator>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();


var app = builder.Build();





// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
