﻿using FluentValidation;
using FluentValidation.Results;
using Framework.Application;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppService.Contracts
{
    public class GetLoanByPersonIdQueryValidation : AbstractValidator<CreateLoanCommand>, ICommandValidator<CreateLoanCommand>
    {
        public new ValidationResult Validate(CreateLoanCommand command)
        {
            var results = base.Validate(command);
            return results;
        }
    }
}
