using Microsoft.EntityFrameworkCore;
using DotNetEnv;
using Microsoft.AspNetCore.Identity;
using System.Text;

using DeviceManagement.Data;
using DeviceManagement.Services;
using Microsoft.IdentityModel.Tokens;


Env.Load();

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DeviceManagementDb>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DeviceManagementDb") ?? throw new InvalidOperationException("Connection string not found."));
});

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<DeviceService>();

builder.Services.AddIdentity<DeviceManagement.Models.User, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddDefaultTokenProviders();

builder.Services.AddCors(options =>
{
    options.AddPolicy("default", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = false;

    // Lockout settings
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
    options.Lockout.MaxFailedAccessAttempts = 10;
    options.Lockout.AllowedForNewUsers = true;

    // User settings
    options.User.RequireUniqueEmail = true;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie settings
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromDays(10);

    // If the LoginPath isn't set, ASP.NET Core defaults the path to /Account/Login.
    options.LoginPath = "/auth/login";
});

builder.Services.AddAuthentication()
.AddJwtBearer(options =>
{
    options.Authority = "http://localhost:5128";
    options.RequireHttpsMetadata = false;

    var envSecretKey = Environment.GetEnvironmentVariable("SECRET_KEY");
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF32.GetBytes(envSecretKey!)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});



builder.Services.AddControllers();


var app = builder.Build();

app.UseHttpsRedirection();
app.UseRouting();

app.UseCors("default");

app.MapStaticAssets();

app.MapControllers();

app.Run();
