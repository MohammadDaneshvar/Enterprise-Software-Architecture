using System.Threading;
using System.Threading.Tasks;

namespace Framework.Application
{
    public class CacheDecoratorCommandHandler<T> : ICommandHandler<T>
    {
        private ICacheProvider cacheProvider;
        private readonly IKeyGenerator _keyGenerator;
        private readonly ICommandHandler<T> _decoratee;

        public CacheDecoratorCommandHandler(ICacheProvider cacheProvider, IKeyGenerator keyGenerator, ICommandHandler<T> decoratee)
        {
            this.cacheProvider = cacheProvider;
            _keyGenerator = keyGenerator;
            _decoratee = decoratee;
        }
        public async Task HandleAsync(T command, CancellationToken cancellationToken)
        {
            var key = _keyGenerator.GenerateKeyForCache(command);
            if (string.IsNullOrEmpty(key))
            {
                await _decoratee.HandleAsync(command, cancellationToken);
                return;
            }
            var result = await cacheProvider.GetAsync(key);
            //if (result != null)
            //    ((IHaveResult)command).Result = result;
            //else
            //{
                await _decoratee.HandleAsync(command, cancellationToken);
            //    await cacheProvider.AddAsync(key, ((IHaveResult)command).Result);

            //}
        }
    }
}