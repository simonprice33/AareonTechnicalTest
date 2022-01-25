using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AareonTechnicalTest.Data.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AareonTechnicalTest.DataHelpers
{
    public class SqLiteContextOptionsBuilder
    {
        private readonly Guid _dbBuilderId;

        public SqLiteContextOptionsBuilder(Guid dbBuilderId)
        {
            _dbBuilderId = dbBuilderId;
        }

        public void BuildDbOptions(DbContextOptionsBuilder builder, IConfiguration configuration)
        {
            BuildDbOptionsInternal(builder, _dbBuilderId);
        }

        public void PostCreate(ApplicationContext dbContext)
        {
            PostCreateInternal(dbContext);
        }

        public static ApplicationContext Create(Guid id)
        {
            var builder = new DbContextOptionsBuilder<ApplicationContext>();
            BuildDbOptionsInternal(builder, id);
            var dbContext = new ApplicationContext(builder.Options);
            PostCreateInternal(dbContext);
            return dbContext;
        }

        private static void PostCreateInternal(ApplicationContext dbContext)
        {
            dbContext.Database.OpenConnection();

            dbContext.Database.EnsureCreated();
        }

        private static void BuildDbOptionsInternal(DbContextOptionsBuilder builder, Guid dbBuilderId)
        {
            builder
                .UseSqlite(CreateConnection(dbBuilderId));
        }

        private static DbConnection CreateConnection(Guid dbBuilderId)
        {
            var connectionString = new SqliteConnectionStringBuilder
            {
                DataSource = dbBuilderId.ToString(),
                Mode = SqliteOpenMode.Memory,
                Cache = SqliteCacheMode.Shared
            };

            var connection = new SqliteConnection(connectionString.ConnectionString);
            return connection;
        }
    }
}