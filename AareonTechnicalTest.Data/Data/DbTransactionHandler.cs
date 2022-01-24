using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AareonTechnicalTest.Application.Commands;
using AareonTechnicalTest.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AareonTechnicalTest.Data.Data
{
    public class DbTransactionHandler : IDbTransactionHandler
    {
        private readonly DbContext _dbContext;

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="dbContext">The DB Context for the current request.</param>
        public DbTransactionHandler(IDbContext dbContext)
        {
            _dbContext = dbContext as DbContext;
        }

        /// <inheritdoc cref=""/>
        public Task BeginTransactionAsync(CancellationToken cancellationToken)
        {
            return _dbContext.Database.BeginTransactionAsync(cancellationToken);
        }

        /// <inheritdoc cref=""/>
        public void CommitTransaction()
        {
            _dbContext.Database.CommitTransaction();
        }

        /// <inheritdoc cref=""/>
        public void RollbackTransaction()
        {
            _dbContext.Database.RollbackTransaction();
        }
    }
}