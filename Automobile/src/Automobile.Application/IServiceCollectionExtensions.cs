using Automobile.Application.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Automobile.Application
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.TryAddScoped<IDataService, DataService>();

            return serviceCollection;
        }
    }
}
