using Framework.Application;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppService.Contracts
{
    public class CreateLoanCommand : IRestrictedCommand,IHaveResult<int>
    {
        public long PersonId { get; set; }
        public string Roles => "admin";
        public string Users => "ali";
        public int  Result { get { return 1; } set { value = 1; } }
    }
}
