using DeviceManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DeviceManagement.Data;

public class DeviceManagementDb : IdentityDbContext<User>
{
    public DeviceManagementDb(DbContextOptions<DeviceManagementDb> options)
        : base(options) { }

    public DbSet<Device> Devices { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties(typeof(Enum)).HaveConversion<string>().HaveColumnType("TEXT");
    }
};
