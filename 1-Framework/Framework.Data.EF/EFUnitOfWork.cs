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
        public void Begin()
        {
            _dbContext.Begin();
        }

        public void Commit()
        {
            _dbContext.Commit();
        }
        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        public void Rollback()
        {
            _dbContext.Rollback();
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