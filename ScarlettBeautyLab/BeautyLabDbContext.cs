using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ScarlettBeautyLab.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScarlettBeautyLab
{
    public class BeautyLabDbContext : IdentityDbContext<UserEntity, UserRoleEntity, Guid>
    {
        public BeautyLabDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ImageEntity> Images { get; set; }
        public DbSet<BrandEntity> Brands { get; set; }
        public DbSet<ProductEntity> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>()
                .Property(c => c.AgeGroup)
                .HasConversion<string>();

            modelBuilder.Entity<UserEntity>()
                .Property(c => c.SkinType)
                .HasConversion<string>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
