using System.Collections.Generic;
using Framework.Application;

namespace AppService.Contracts
{
    public class EditOrderCommand : IRestrictedCommand
    {
        public long CustomerId { get; set; }
        public List<OrderLineDto> OrderLines { get; set; }
        public string Roles => "admin";
        public string Users => "ali";
    }
}