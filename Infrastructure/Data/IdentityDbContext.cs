using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class IdentityDbContext : IdentityDbContext<User, IdentityRole, string>
    {
        override public DbSet<User> Users => Set<User>();
        public DbSet<Condition> Conditions => Set<Condition>();
        public DbSet<Device> Devices => Set<Device>();
        public DbSet<Event> Events => Set<Event>();
        public DbSet<Hub> Hubs => Set<Hub>();
        public DbSet<Notification> Notifications => Set<Notification>();
        public DbSet<Sensor> Sensors => Set<Sensor>();
        public DbSet<SensorData> SensorDatas => Set<SensorData>();
        public DbSet<SensorType> SensorTypes => Set<SensorType>();
        public DbSet<Trigger> Triggers => Set<Trigger>();

        public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Hub>().HasIndex(hub => hub.MacAddress).IsUnique();
            base.OnModelCreating(builder);
        }


    }
}
