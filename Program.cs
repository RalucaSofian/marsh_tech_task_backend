using Microsoft.EntityFrameworkCore;

using DeviceManagement.Data;
using DeviceManagement.Services;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DeviceManagementDb>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DeviceManagementDb") ?? throw new InvalidOperationException("Connection string not found."));
});

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<DeviceService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("default", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

builder.Services.AddControllers();


var app = builder.Build();

app.UseHttpsRedirection();
app.UseRouting();

app.UseCors("default");

app.MapStaticAssets();

app.MapControllers();

app.Run();
