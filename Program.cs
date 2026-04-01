using DeviceManagement.Data;
using DeviceManagement.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DeviceManagementDb>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DeviceManagementDb") ?? throw new InvalidOperationException("Connection string not found."));
});

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<DeviceService>();

builder.Services.AddControllers();


var app = builder.Build();

app.UseHttpsRedirection();
app.UseRouting();

app.MapStaticAssets();

app.MapControllers();

app.Run();
