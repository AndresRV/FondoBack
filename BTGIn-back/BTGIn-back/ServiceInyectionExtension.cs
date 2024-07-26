using BTGIn_back.Business.Contracts;
using BTGIn_back.Business.Implement;
using BTGIn_back.Repositories.Contracts;
using BTGIn_back.Repositories.Implement;

namespace BTGIn_back
{
    public static class ServiceInyectionExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {

            services.AddTransient<IClientService, ClientService>();

            services.AddScoped<IClientRepository, ClientRepository>();

            return services;
        }
    }
}
