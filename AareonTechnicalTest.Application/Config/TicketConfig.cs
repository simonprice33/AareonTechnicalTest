using AareonTechnicalTest.Application.Entities;
using Microsoft.EntityFrameworkCore;

namespace AareonTechnicalTest.Application.Config
{
    public static class TicketConfig
    {
        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>(
                entity =>
                {
                    entity.HasKey(e => e.Id);
                });
        }
    }
}