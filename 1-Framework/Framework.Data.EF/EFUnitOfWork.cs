using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Framework.Data.EF
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly IDbContext _dbContext;
        public EFUnitOfWork(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task BeginAsync()
        {
            await _dbContext.BeginAsync();
        }

        public async Task CommitAsync()
        {
            await _dbContext.CommitAsync();
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public async Task RollbackAsync()
        {
          await   _dbContext.RollbackAsync();
        }

  
        //public Task<IDbContextTransaction> BeginAsync()
        //{
        //  return   await _dbContext.Database.BeginTransactionAsync();
        //}

        //public void CommitAsync()
        //{
        //    _dbContext.Database.CommitTransaction();
        //}
        //public int SaveChangesAsync()
        //{
        //    return _dbContext.SaveChanges();
        //}

        //public void RollbackAsync()
        //{
        //    _dbContext.Database.RollbackTransaction();
        //}
    }
}