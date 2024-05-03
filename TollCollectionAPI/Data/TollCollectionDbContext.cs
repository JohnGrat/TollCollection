using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class TollCollectionDbContext : DbContext
    {
        public TollCollectionDbContext(DbContextOptions<TollCollectionDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure your model here
        }
    }
}
