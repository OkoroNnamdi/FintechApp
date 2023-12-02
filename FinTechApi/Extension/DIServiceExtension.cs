using FinTechCore.Implementations;
using FinTechCore.Interfaces;
using FinTechCore.Utilities;

namespace FinTechApi.Extension
{
    public static  class DIServiceExtension
    {
        public static void AddDependencyInjection(this IServiceCollection services)
        {
            services.AddTransient<ICurrencyAPI, CurrentApiService>();
            services.AddTransient<IAcccountService, AccountService>();
            services.AddTransient<ITransaction, TransactionServices>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ITokenDetails, TokenDetails>();
        }
    }
}
