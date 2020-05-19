using Domain.Logs;
using Domain.Person;
using Framework.Data;
using Infra.Persistance.EF.Logs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Persistance.EF
{
    public class FRIDbContext : DbContext, IDbContext
    {
        public DbSet<Log> Logs { get; set; }
        public FRIDbContext():base(GetOptions("Data Source=.;Initial Catalog=FRI;User ID=sa;Password=123456"))
        {

        }
        public FRIDbContext(string connectionString) : base(GetOptions(connectionString))
        {

        }
        protected internal virtual void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FRIDbContext).Assembly); // Here UseConfiguration is any IEntityTypeConfiguration
            modelBuilder.ApplyConfiguration(new LogConfiguration());

        }

        public async Task BeginAsync()
        {
            await Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            Database.CommitTransaction();
        }

        public async Task RollbackAsync()
        {
            await Database.BeginTransactionAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            var result = await SaveChangesAsync();
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



        public IQueryable<T> Query<T>() where T : class
        {
            return base.Query<T>();
        }

        public async Task AddRangeAsync<T>(IEnumerable<T> items) where T : class
        {
            await base.AddRangeAsync(items);
        }

        public async Task AddAsync<T>(T entity) where T : class
        {
            await base.AddAsync(entity);
        }

        public new async Task<TEntity> FindAsync<TEntity, TKey>(TKey id) where TEntity : class
        {
            return await base.FindAsync<TEntity>(id);
        }
    }
}
