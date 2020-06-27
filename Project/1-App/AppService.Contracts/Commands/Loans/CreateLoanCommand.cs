using Framework.Application;
using Framework.Application.Common.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppService.Contracts
{
    [CommandRoute("CreateLoanCommand")]
    public class CreateLoanCommand : IRestrictedCommand, IHaveResult<int>
    {
        public long PersonId { get; set; }
        [JsonIgnore]
        public string Roles => "admin";
        [JsonIgnore]
        public string Users => "ali";
        public int Result { get; set; }
    }
}
