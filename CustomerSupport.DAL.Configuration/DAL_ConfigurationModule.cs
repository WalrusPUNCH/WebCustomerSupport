using System;
using Microsoft.Extensions.DependencyInjection;
using CustomerSupport.DAL.Abstract;
using CustomerSupport.DAL.Impl;

namespace CustomerSupport.DAL.Configuration
{
    public static class DAL_ConfigurationModule
    {
        public static IServiceCollection UseDAModule(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
