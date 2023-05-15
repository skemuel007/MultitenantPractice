using Core.Contracts;
using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Hangfire;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        private string TenantId { get; set; }
        private readonly ITenantService _tenantService;
        private readonly IHangfireTenantProvider _hangfireTenantProvider;

        public ApplicationDbContext(DbContextOptions options, ITenantService tenantService,
            IHangfireTenantProvider hangfireTenantProvider) : base(options)
        {
            _tenantService = tenantService;
            TenantId = _tenantService.GetTenant()?.TID;
            _hangfireTenantProvider = hangfireTenantProvider;
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>()
                .HasQueryFilter(a => a.TenantId == TenantId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var tenantConnectonString = _tenantService.GetConnectionString();
            if (!string.IsNullOrEmpty(tenantConnectonString))
            {
                var databaseProvider = _tenantService.GetDatabaseProvider();
                if(databaseProvider.ToLower() == "mssql")
                {
                    optionsBuilder.UseSqlServer(_tenantService.GetConnectionString());
                }
            }
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach(var entry in ChangeTracker.Entries<IMustHaveTenant>().ToList())
            {
                switch(entry.State)
                {
                    case EntityState.Added:
                    case EntityState.Modified:
                        entry.Entity.TenantId = TenantId;
                        break;
                }
            }
            var result = await base.SaveChangesAsync(cancellationToken);
            return result;
        }

        public override int SaveChanges()
        {
            foreach(var entry in ChangeTracker.Entries<IMustHaveTenant>().ToList())
            {
                switch(entry.State)
                {
                    case EntityState.Added:
                    case EntityState.Modified:
                        entry.Entity.TenantId = TenantId;
                        break;
                }
            }
            return base.SaveChanges();
        }
    }
}
