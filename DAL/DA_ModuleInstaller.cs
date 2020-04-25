using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using DAL.Interfaces;

namespace DAL
{
    public static class DA_ModuleInstaller
    {
        public static IServiceCollection UseDAModule(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddSingleton<IUnitOfWork, UnitOfWork>();
        }
    }
}
