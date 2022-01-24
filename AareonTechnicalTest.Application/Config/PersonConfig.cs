using AareonTechnicalTest.Application.Entities;
using Microsoft.EntityFrameworkCore;

namespace AareonTechnicalTest.Application.Config
{
    public static class PersonConfig
    {
        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ticket>(
                entity =>
                {
                    entity.HasKey(e => e.Id);
                });
        }
    }
}