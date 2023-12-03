using FinTechCore.Implementations;
using FinTechCore.Interfaces;
using FinTechCore.IService;
using FinTechCore.Repository;
using FinTechCore.Service;
using FinTechCore.Utilities;

namespace FinTechApi.Extension
{
    public static  class DIServiceExtension
    {
        public static void AddDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<ICurrencyAPI, CurrentApiService>();
            services.AddScoped<IAcccountService, AccountService>();
            services.AddScoped<ITransaction, TransactionServices>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ITokenDetails, TokenDetails>();
            services.AddScoped<IEmailService,EmailService>();

            //Add repository 
            services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
        }
    }
}
