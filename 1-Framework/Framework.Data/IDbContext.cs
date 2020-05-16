using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Data
{
    public interface IDbContext
    {
        Task BeginAsync();
        Task CommitAsync();
        Task<int> SaveChangesAsync();
        Task RollbackAsync();
    }
}
