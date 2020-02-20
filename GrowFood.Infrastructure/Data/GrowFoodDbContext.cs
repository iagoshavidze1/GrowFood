using GrowFood.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrowFood.Infrastructure.Data
{
    public class GrowFoodDbContext : DbContext
    {
        public GrowFoodDbContext(DbContextOptions<GrowFoodDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new UserTypeConfiguration());
        }
    }
}
