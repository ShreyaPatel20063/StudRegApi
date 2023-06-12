//using Microsoft.Extensions.DependencyInjection;   // --not required
global using SutdManagmentSysAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<StudentsDbContext>();
builder.Services.AddCors(cors => cors.AddPolicy("Allowed", abc => //for adding cors = cross origin resourse sharing
{
    abc.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod() ;
}));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//app.UseRouting();
app.UseCors("Allowed");

app.UseAuthorization();

app.MapControllers();

app.Run();
