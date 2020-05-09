using System.Threading.Tasks;

namespace Framework.Data.EF
{
    public interface IUnitOfWork
    {
        void Begin();
        void Commit();
        void Rollback();
        int SaveChanges();
        //void Task BeginAsync();
        //void Task CommitAsync();
        //void Task RollbackAsync();
        //Task<int> SaveChangesAsync();
    }
}