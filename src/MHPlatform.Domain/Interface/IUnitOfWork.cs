using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHPlatform.Domain.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IOrderFormRepository OrderForm { get; }
        IUserRepository User { get; }

        IUserClaimRepository UserClaim { get; }

        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
