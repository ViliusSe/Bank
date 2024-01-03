using Application.Interfaces;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructureToDIContainer(this IServiceCollection service)
        {
            service.AddScoped<IUserRepository, UserRepository>();
            service.AddScoped<IUserRepository, UserRepository>();
            service.AddScoped<IAccountRepository, AccountRepository>();
            service.AddScoped<ITransactionRepository, TransactionRepository>();

            
        }

    }
}
