using backend.Communication.Repos;
using backend.Communication.Services;
using backend.Configuration;
using backend.DataLayer;
using backend.Repos;
using backend.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var bootstrap = new Bootstrapper();
//var appContext = new ApplicationContext();

bootstrap.ConfigureServices(builder.Services);
builder.Services.AddSingleton<ApplicationContext>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();