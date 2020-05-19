using Domain.Loans;
using Infra.Persistance.EF;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppService.Query.Finders.Loans
{
    public class LoanFinder : ILoanFinder
    {
        private readonly FRIQueryDbContext _dbContext;

        public LoanFinder(FRIQueryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Contract> FindByPersonIdAsync(long personId)
        {
            //below text in Example for call Store procedure

            //var catName = "Personal Care";
            //_dbContext.Database
            //           .ExecuteSqlCommandAsync("usp_InsertCategory @p0", catName);

            //return await _entities.Query
            return new Contract();
        }
    }
}
