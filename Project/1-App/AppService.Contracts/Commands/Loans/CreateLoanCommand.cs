using Framework.Application;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppService.Contracts
{
    public class CreateLoanCommand : IRestrictedCommand
    {
        public long PersonId { get; set; }
        public string Roles => "admin";
        public string Users => "ali";
    }
}
