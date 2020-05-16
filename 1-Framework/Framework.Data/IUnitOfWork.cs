using System.Threading.Tasks;

namespace Framework.Data.EF
{
    public interface IUnitOfWork
    {
        Task BeginAsync();
        Task CommitAsync();
        Task RollbackAsync();
        Task<int> SaveChangesAsync();
        //void Task BeginAsync();
        //void Task CommitAsync();
        //void Task RollbackAsync();
        //Task<int> SaveChangesAsync();
    }
}