using Framework.Application;

namespace AppService.Contracts.Queries
{
    public class AllOrderQuery
    {

    }

    public class QueryBase : IHaveResult
    {
        public object Result { get; set; }

    }
    [CacheOutput]
    public class GetOrderByIdQuery : IHaveResult
    {
        [CacheParameter]
        public long Id { get; set; }
        public object Result { get; set; }
    }
}