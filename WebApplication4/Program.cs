using System.Data.SqlClient;
using WebApplication3;
using WebApplication3.DTOs;

using FluentValidation;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddValidatorsFromAssemblyContaining<CreateAnimalValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateAnimalRequest>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.RegisterAnimalsEndpoints();


app.UseHttpsRedirection();
app.Run();

