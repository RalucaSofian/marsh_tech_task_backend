using DeviceManagement.Models;
using Microsoft.EntityFrameworkCore;


namespace DeviceManagement.Data;

public class DeviceManagementDb : DbContext
{
    public DeviceManagementDb(DbContextOptions<DeviceManagementDb> options)
        : base(options) { }

    public DbSet<Device> Devices { get; set; } = default!;
    public DbSet<User> Users { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(ent => ent.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.SetNull;
        }
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties(typeof(Enum)).HaveConversion<string>().HaveColumnType("TEXT");
    }
};
