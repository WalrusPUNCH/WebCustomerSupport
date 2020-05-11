using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

using CustomerSupport.BL.Abstract;
using CustomerSupport.BL.Services;

using CustomerSupport.BL.Services.Mapper.Abstract;
using CustomerSupport.BL.Services.Mapper;

namespace CustomerSupport.BL.Configuration
{
    public static class BL_ConfigurationModule
    {
        public static IServiceCollection UseBLModules(this IServiceCollection services)
        {
            services.AddScoped<ISpecialistManagementService, SpecialistManagementService>();
            services.AddScoped<IRequestManagementService, RequestManagementService>();
            services.AddScoped<IRequestInfoService, RequestManagementService>();
            services.AddSingleton<IBLMapper, BLMapper>();
            return services;
        }
    }

}
