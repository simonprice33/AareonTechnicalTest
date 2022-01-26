using System;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using AareonTechnicalTest.Application.Commands;
using AareonTechnicalTest.Application.Config;
using AareonTechnicalTest.Application.Entities;
using AareonTechnicalTest.Application.Queries;
using Microsoft.EntityFrameworkCore;

namespace AareonTechnicalTest.Data.Data
{
    public class ApplicationContext : DbContext, IDbContext, IReadOnlyDbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            var envDir = Environment.CurrentDirectory;

            DatabasePath = $"{envDir}{System.IO.Path.DirectorySeparatorChar}Ticketing.db";
        }

        public virtual DbSet<Person> Persons { get; set; }

        public virtual DbSet<Ticket> Tickets { get; set; }

        public Task<int> SaveChangesAsync() => base.SaveChangesAsync();

        public string DatabasePath { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite($"Data Source={DatabasePath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            PersonConfig.Configure(modelBuilder);
            TicketConfig.Configure(modelBuilder);
        }
    }
}