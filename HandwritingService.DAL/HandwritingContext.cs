using HandwritingService.Domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace HandwritingService.DAL
{
    public class HandwritingContext : DbContext
    {
        public DbSet<Handwriting> Handwritings { get; set; }
        public DbSet<Note> Notes { get; set; }

        public HandwritingContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Apply configs

            base.OnModelCreating(modelBuilder);
        }
    }
}
