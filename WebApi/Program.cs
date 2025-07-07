using Application.Installers;
using Services.Installers;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddApiVersioning();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.InstallApplication();
builder.Services.InstallServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
