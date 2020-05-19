using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Loans;
using Infra.Persistance.EF;
using Microsoft.EntityFrameworkCore;

namespace AppService.Query.Finders.Loans
{
    public interface ILoanFinder
    {
        Task<Contract> FindByPersonIdAsync(long personId);
    }

   
}