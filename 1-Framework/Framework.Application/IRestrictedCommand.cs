using Framework.Application.Common.Attributes;
using Newtonsoft.Json;

namespace Framework.Application
{
    public interface IRestrictedCommand
    {
        [JsonIgnore]
        [SwaggerIgnore]
        string Roles { get; }
        [SwaggerIgnore]
        [JsonIgnore]
        string Users { get; }

    }
}