using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;
using AutoMapper;

using DAL.Entities;
using BLL.Models;
using BLL.Services;
using BLL.Interfaces;

namespace BLL
{
    public static class BL_ModuleInstaller
    {
        public static IServiceCollection UseBLModules(this IServiceCollection services)
        {
            services.AddScoped<ISpecialistManagementService, SpecialistsManagementService>();
            services.AddScoped<IStatisticsService, StatisticsService>();
            services.AddScoped<IRequestService, RequestManagerService>();
            services.AddScoped<IGetRequestsService, RequestsInformationService>();
            return services;
        }
    }

}
