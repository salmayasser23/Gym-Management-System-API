using GymManagementSystem.Api.Helpers;
using GymManagementSystem.Api.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddApplicationDatabase(builder.Configuration);
builder.Services.AddApplicationIdentity();
builder.Services.AddApplicationJwtAuthentication(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddApplicationSwagger();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
await DbInitializer.InitializeAsync(app.Services);
app.Run();