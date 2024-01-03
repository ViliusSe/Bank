using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static void AddApplicationsToDIContainer(this IServiceCollection serviceProvider)
        {
            serviceProvider.AddScoped<UserService>();
            serviceProvider.AddScoped<AccountService>();
            serviceProvider.AddScoped<TransactionService>();
        }

    }
}
