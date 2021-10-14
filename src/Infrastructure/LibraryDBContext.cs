

using library_api.Infrastructure.Mapping;
using library_api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace library_api.Infrastructure
{
    public class LibraryDBContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public LibraryDBContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*  
               modelBuilder.HasCollation("my_collation_deterministic", locale: "en-u-ks-primary", provider: "icu", deterministic: true);
              modelBuilder.HasCollation("my_collation", locale: "en-u-ks-primary", provider: "icu", deterministic: false);
            */

            modelBuilder.HasCollation("my_collation", locale: "en-u-ks-primary", provider: "icu", deterministic: false);
            modelBuilder.UseDefaultColumnCollation("my_collation");

            modelBuilder.HasPostgresExtension("uuid-ossp")
                               .Entity<Book>()
                               .Property(e => e.Id)
                               .HasDefaultValueSql("uuid_generate_v4()");

            modelBuilder.HasPostgresExtension("uuid-ossp")
                                          .Entity<Author>()
                                          .Property(e => e.Id)
                                          .HasDefaultValueSql("uuid_generate_v4()");

            modelBuilder.HasPostgresExtension("uuid-ossp")
                                             .Entity<Publisher>()
                                             .Property(e => e.Id)
                                             .HasDefaultValueSql("uuid_generate_v4()");

            /* 
             modelBuilder.HasPostgresExtension("uuid-ossp")
                                                         .Entity<Publisher>()
                                                         .Property(e => e.Name)
                                                         .UseCollation("my_collation_deterministic");
            */

            modelBuilder.ApplyConfiguration(new BookMapping());


            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("LibraryDBContext"));
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.LogTo(Console.WriteLine);

            base.OnConfiguring(optionsBuilder);
        }
        
        DbSet<Book> Books { get; set; }
        DbSet<Publisher> Publishers { get; set; }
        DbSet<Author> Authors { get; set; }
    }
}
