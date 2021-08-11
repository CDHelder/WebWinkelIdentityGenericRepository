using Microsoft.Extensions.DependencyInjection;
using WebWinkelIdentity.Data.Service;
using WebWinkelIdentity.Data.Service.Interfaces;

namespace WebWinkelIdentity.Web.ServicesExtensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection ConfigureAllDependencyInjections(this IServiceCollection services)
        {
            services.AddScoped(typeof(IUnitOfWork),typeof(UnitOfWork));
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            return services;
        }
    }
}
