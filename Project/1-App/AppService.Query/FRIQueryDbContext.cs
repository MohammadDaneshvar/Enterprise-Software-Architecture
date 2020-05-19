using Domain.Person;
using Framework.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Persistance.EF
{
    public class FRIQueryDbContext : DbContext
    {
        private DbContext _dbContext { get; set; }
        private FRIQueryDbContext()
        {
        }
        public FRIQueryDbContext(string connectionString) : base(GetOptions(connectionString))
        {
        }
        protected internal virtual void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FRIDbContext).Assembly); // Here UseConfiguration is any IEntityTypeConfiguration
        }
        private static DbContextOptions GetOptions(string connectionString)
        {
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), connectionString).Options;
        }
    }
}
