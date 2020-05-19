﻿using Framework.Application;
using AppService.Contracts.Queries;
using System.Threading.Tasks;
using AppService.Query.Finders.Loans;
using AppService.Contracts.Queries.Loans;

namespace AppService.Queries
{
    public class loanQueryHandlers : ICommandHandler<GetLoanByPersonIdQuery>
    {
        private readonly ILoanFinder _loanFinder;

        public loanQueryHandlers(ILoanFinder loanFinder)
        {
            _loanFinder = loanFinder;
        }
        public async Task HandleAsync(GetLoanByPersonIdQuery command) => command.Result = _loanFinder.FindByPersonIdAsync(command.Id);

    }
}