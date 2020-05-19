using Framework.Application;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppService.Contracts.Queries.Loans
{
    public class GetLoanByPersonIdQuery : IHaveResult
    {
        public long Id { get; set; }
        public object Result { get; set; }
    }
}
