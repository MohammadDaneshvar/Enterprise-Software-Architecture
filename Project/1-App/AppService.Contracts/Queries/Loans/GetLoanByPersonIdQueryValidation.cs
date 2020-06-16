using FluentValidation;
using FluentValidation.Results;
using Framework.Application;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppService.Contracts.Queries.Loans
{
    public class GetLoanByPersonIdQueryValidation : AbstractValidator<GetLoanByPersonIdQuery>, ICommandValidator<GetLoanByPersonIdQuery>
    {
     
    }
}
