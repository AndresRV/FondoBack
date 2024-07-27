using BTGIn_back.Business.Contracts;
using BTGIn_back.Business.Implement;
using BTGIn_back.Repositories;
using BTGIn_back.Repositories.Contracts;
using BTGIn_back.Repositories.Implement;

namespace BTGIn_back
{
    public static class ServiceInyectionExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IClientTransactionsService, ClientTransactionsService>();

            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IFundRepository, FundRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();

            services.AddScoped<DatabaseInitializer>();

            return services;
        }
    }
}
