using System.ServiceModel;

namespace Framework.Application
{
    [ServiceContract]
    public interface ICommandHandlerService<TCommand>
    {
        [OperationContract]
        string Send(TCommand command);
    }
}