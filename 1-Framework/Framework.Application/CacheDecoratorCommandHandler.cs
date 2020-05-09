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

        public void Handle(T command)
        {
            var key = _keyGenerator.GenerateKeyForCache(command);
            if (string.IsNullOrEmpty(key))
            {
                _decoratee.Handle(command);
                return;
            }
            var result = cacheProvider.Get(key);
            if (result != null)
                ((IHaveResult)command).Result = result;
            else
            {
                _decoratee.Handle(command);
                cacheProvider.Add(key, ((IHaveResult)command).Result);
            }
        }
    }
}