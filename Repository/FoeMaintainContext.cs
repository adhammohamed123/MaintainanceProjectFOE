using Contracts.Base;
using Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;

namespace Repository
{
    public class FoeMaintainContext:IdentityDbContext<User>
    {

        public DbSet<Department>  Departments { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<DeviceFailureHistory> Maintainances { get; set; }
        public DbSet<Failure> Failures { get; set; }
        public DbSet<FailureMaintain> FailureMaintains { get; set; }
        public DbSet<Gate> Gates { get; set; }
        public DbSet<Office> Offices { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
       



        public FoeMaintainContext(DbContextOptions<FoeMaintainContext> options) : base(options) { }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {

            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is FullAduitbaseModel referece)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                             referece.CreatedDate = DateTime.Now;
                            break;
                        case EntityState.Deleted:
                        case EntityState.Modified:
                            referece.LastModifiedDate = DateTime.Now;
                            break;
                    default:
                            break;
                    }
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<FailureMaintain>()
                .HasKey(f => new { f.FailureId, f.DeviceFailureHistoryId });

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            // Apply global query filter to all entities implementing ISoftDeletedModel
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(ISoftDeletedModel).IsAssignableFrom(entityType.ClrType))
                {
                    var parameter = Expression.Parameter(entityType.ClrType, "e");
                    var property = Expression.Property(parameter, nameof(ISoftDeletedModel.IsDeleted));
                    var filter = Expression.Lambda(Expression.Equal(property, Expression.Constant(false)), parameter);
                    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(filter);
                }
            }
  
        }

    }
}
