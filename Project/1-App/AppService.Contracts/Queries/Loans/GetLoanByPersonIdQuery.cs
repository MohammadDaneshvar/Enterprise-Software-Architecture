using Framework.Application;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppService.Contracts.Queries.Loans
{
    public class GetLoanByPersonIdQuery<TResult> : IHaveResult<TResult>
    {
        public long Id { get; set; }
        public TResult Result { get; set; }
    }
}
