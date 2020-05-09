using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Newtonsoft.Json;

namespace Framework.Application
{
    public class RemoteCommandHandler<T> : ICommandHandler<T>
    {
        public RemoteCommandHandler()
        {
        }
        public void Handle(T command)
        {
            var address = new Uri($"http://{"localhost"}:{"228"}/{command.GetType().Name}");
            var basicHttpBinding = new BasicHttpBinding { MaxReceivedMessageSize = 2147483647 };

            var factory = new ChannelFactory<ICommandHandlerService<T>>(basicHttpBinding, new EndpointAddress(address));
            var client = factory.CreateChannel();
            var result = client.Send(command);
            if (result != null)
            {
                ((IHaveResult)command).Result = JsonConvert.DeserializeObject(result, new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.All,
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
            }
        }
    }

}