﻿using System;
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
        }

        public virtual DbSet<Person> Persons { get; set; }

        public virtual DbSet<Ticket> Tickets { get; set; }

        public Task<int> SaveChangesAsync() => base.SaveChangesAsync();

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