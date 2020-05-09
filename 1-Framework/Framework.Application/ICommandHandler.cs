using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Application
{
    public interface ICommandHandler<in T>
    {
        void Handle(T command);
    }
}
