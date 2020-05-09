using Framework.Data.EF;

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
        public void Handle(T command)
        {
            _unitOfWork.Begin();
            _decoratee.Handle(command);
            _unitOfWork.Commit();
        }
    }
}