using Microsoft.Extensions.Configuration;

namespace PossumLabs.DSL.Core.Configuration
{
    public interface IConfigurationFactory
    {
        IConfiguration Configuration { get; }

        T Create<T>();
    }
}