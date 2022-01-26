using System;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using AareonTechnicalTest.Application.Commands;
using AareonTechnicalTest.Application.Config;
using AareonTechnicalTest.Application.Entities;
using AareonTechnicalTest.Application.Queries;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AareonTechnicalTest.Data.Data
{
    public class ApplicationContext : DbContext, IDbContext, IReadOnlyDbContext
    {
        private readonly bool _configure;

        public ApplicationContext(DbContextOptions<ApplicationContext> options, bool configure = true)
            : base(options)
        {
            _configure = configure;
            var envDir = Environment.CurrentDirectory;

            DatabasePath = $"{envDir}{System.IO.Path.DirectorySeparatorChar}Ticketing.db";
        }

        public virtual DbSet<Person> Persons { get; set; }

        public virtual DbSet<Ticket> Tickets { get; set; }

        public Task<int> SaveChangesAsync()
        {
            var addedEntities = this.ChangeTracker
                .Entries()
                .Where(e => e.State == EntityState.Added)
                .ToArray();
            this.EnsureAutoHistory();
            base.SaveChanges();

            this.EnsureAddedHistory(addedEntities);
            return base.SaveChangesAsync();
        }

        public string DatabasePath { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            //// This line conflicts with tests when uncommented, and needs to be uncommented for migrations.
            ////if (System.Diagnostics.Debugger.IsAttached == false) System.Diagnostics.Debugger.Launch();
            if (_configure)
            {
                options.UseSqlite($"Data Source={DatabasePath}");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.EnableAutoHistory();

            PersonConfig.Configure(modelBuilder);
            TicketConfig.Configure(modelBuilder);
        }
    }
}