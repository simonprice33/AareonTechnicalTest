using System;
using AareonTechnicalTest.Application.Commands;
using AareonTechnicalTest.Application.Config;
using AareonTechnicalTest.Application.Entities;
using Microsoft.EntityFrameworkCore;

namespace AareonTechnicalTest.Data.Data
{
    public class ApplicationContext : DbContext, IDbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Person> Persons { get; set; }

        public virtual DbSet<Ticket> Tickets { get; set; }

        public string DatabasePath { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            PersonConfig.Configure(modelBuilder);
            TicketConfig.Configure(modelBuilder);
        }
    }
}