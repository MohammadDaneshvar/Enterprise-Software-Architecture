using System;
using System.Collections.Generic;
using System.Text;

namespace AppService.Contracts.Dtos
{
    public class LoanDto
    {
        public long ContractId { get; set; }
        public string ContractType { get; set; }
    }
}
