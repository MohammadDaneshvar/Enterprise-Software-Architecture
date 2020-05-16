using Framework.Data.EF;
using System.Threading.Tasks;

namespace Framework.Application
{
    public class TransactionalCommandHandler<T> : ICommandHandler<T>
    {
        private readonly ICommandHandler<T> _decoratee;
        private readonly IUnitOfWork _unitOfWork;

        public TransactionalCommandHandler(ICommandHandler<T> decoratee, IUnitOfWork unitOfWork)
        {
            _decoratee = decoratee;
            _unitOfWork = unitOfWork;
        }
        public async Task HandleAsync(T command)
        {
            _unitOfWork.BeginAsync();
            await _decoratee.HandleAsync(command);
            _unitOfWork.CommitAsync();
        }
    }
}