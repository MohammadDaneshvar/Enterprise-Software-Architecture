using Framework.Application;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppService.Contracts.Commands.Loans
{
    public class CreateLoanCommandValidation<CreateLoanCommand> : ICommandValidator<CreateLoanCommand>
    {
        public void Validate(CreateLoanCommand command)
        {
           // throw new NotImplementedException();
        }
    }
}
