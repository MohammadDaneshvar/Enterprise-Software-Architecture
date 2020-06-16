using Domain.Logs;
using Domain.Person;
using Framework.Data;
using Infra.Persistance.EF.Logs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infra.Persistance.EF
{
    public class FRIDbContext : DbContext, IDbContext
    {
        public DbSet<Log> Logs { get; set; }
        //public FRIDbContext():base(GetOptions("Data Source=.;Initial Catalog=FRI;User ID=sa;Password=123456"))
        //{

        //}
        public FRIDbContext(DbContextOptions options) : base(options)
        {

        }
        public FRIDbContext()
        {

        }
        //protected internal virtual void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //    modelBuilder.ApplyConfigurationsFromAssembly(typeof(FRIDbContext).Assembly); // Here UseConfiguration is any IEntityTypeConfiguration
        //    modelBuilder.ApplyConfiguration(new LogConfiguration());

        //}

        public async Task BeginAsync(CancellationToken cancellationToken = default)
        {
            await Database.BeginTransactionAsync(cancellationToken);
        }

        public  void Commit()
        {
            Database.CommitTransaction();
        }

        public async Task RollbackAsync(CancellationToken cancellationToken = default)
        {
            await Database.BeginTransactionAsync(cancellationToken);
            
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken= default)
        {
            var result = await SaveChangesAsync(cancellationToken);
            return result;
        }
        private static DbContextOptions GetOptions(string connectionString)
        {
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), connectionString).Options;
        }

        public void RemoveRange<T>(IEnumerable<T> entities) where T : class
        {
            RemoveRange(entities);
        }

        void IDbContext.Remove<T>(T entity)
        {
            Remove(entity);
        }

        void IDbContext.Update<T>(T entity)
        {
            Update(entity);
        }



        public new IQueryable<T> Query<T>() where T : class
        {
            return base.Query<T>();
        }

        public async Task AddRangeAsync<T>(IEnumerable<T> items, CancellationToken cancellationToken = default) where T : class
        {
            await base.AddRangeAsync(items, cancellationToken);
        }

        public async Task AddAsync<T>(T entity, CancellationToken cancellationToken = default) where T : class
        {
            await base.AddAsync(entity, cancellationToken);
        }

        public new async Task<TEntity> FindAsync<TEntity, TKey>(TKey id, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await base.FindAsync<TEntity>(id, cancellationToken);
        }
    }
}
