using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AareonTechnicalTest.Shared.Interfaces
{
    public interface IDbTransactionHandler
    {
        /// <summary>
        ///     Begin a transaction.
        /// </summary>
        Task BeginTransactionAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Commit a transaction.
        /// </summary>
        void CommitTransaction();

        /// <summary>
        /// Rollback a transaction.
        /// </summary>
        void RollbackTransaction();
    }
}