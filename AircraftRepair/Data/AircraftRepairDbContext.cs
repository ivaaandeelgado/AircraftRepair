using Microsoft.EntityFrameworkCore;
using AircraftRepair.Entities;

namespace AircraftRepair.Data;

public class AircraftRepairDbContext : DbContext
{
    public AircraftRepairDbContext(DbContextOptions<AircraftRepairDbContext> options)
        : base(options)
    {
    }

    public DbSet<AppUser> AppUsers => Set<AppUser>();
    public DbSet<TaskItem> Tasks => Set<TaskItem>();
    public DbSet<TaskState> TaskStates => Set<TaskState>();
    public DbSet<Assignment> Assignments => Set<Assignment>();
    public DbSet<Permission> Permissions => Set<Permission>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Seed inicial de estados
        modelBuilder.Entity<TaskState>().HasData(
            new TaskState { IdState = 1, Code = 1, Value = "pending" },
            new TaskState { IdState = 2, Code = 2, Value = "inProgress" },
            new TaskState { IdState = 3, Code = 3, Value = "done" }
        );
        modelBuilder.Entity<Permission>().HasData(
             new Permission { IdPermission = 1, Code = 1, Value = "Admin" },
             new Permission { IdPermission = 2, Code = 2, Value = "User" }
        );

        base.OnModelCreating(modelBuilder);
    }

}