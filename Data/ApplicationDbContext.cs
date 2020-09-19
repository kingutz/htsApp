using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using htsApp.Models;
using htsApp.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace htsApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly ICurrentUserService currentUserService;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ICurrentUserService currentUserService)
            : base(options)
        {
            this.currentUserService = currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));

        }
        public DbSet<HTSData> HTS { get; set; }
        public DbSet<District> district { get; set; }
        public DbSet<Shehia> shehia { get; set; }
        public DbSet<Facility> Facility { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ProcessSave();
            return base.SaveChangesAsync(cancellationToken);
        }
        private void ProcessSave()
        {
            var currentTime = DateTimeOffset.UtcNow;
            foreach (var item in ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added && e.Entity is EntityData))
            {
                var entidad = item.Entity as EntityData;
                entidad.CreatedDate = currentTime;
                entidad.CreatedByUser = currentUserService.GetCurrentUsername();
                entidad.ModifiedDate = currentTime;
                entidad.ModifiedByUser = currentUserService.GetCurrentUsername();
            }

            foreach (var item in ChangeTracker.Entries()
                .Where(predicate: e => e.State == EntityState.Modified && e.Entity is EntityData))
            {
                var entidad = item.Entity as EntityData;
                entidad.ModifiedDate = currentTime;
                entidad.Edited = true;
                entidad.ModifiedByUser = currentUserService.GetCurrentUsername();
                item.Property(nameof(entidad.CreatedDate)).IsModified = false;
                item.Property(nameof(entidad.CreatedByUser)).IsModified = false;
            }
        }
       

       
    }
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        public string Name { get; set; }
    }
}
