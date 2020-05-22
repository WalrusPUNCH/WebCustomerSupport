using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

using CustomerSupport.DAL.Entities;
using CustomerSupport.BL.Abstract;
using CustomerSupport.BL.Abstract.Mapper;
using CustomerSupport.BL.Services;
using CustomerSupport.BL.Services.Mapper;
using CustomerSupport.BL.DTOs;

namespace CustomerSupport.BL.Configuration
{
    public static class BL_ConfigurationModule
    {
        public static IServiceCollection UseBLModules(this IServiceCollection services)
        {
            services.AddScoped<ISpecialistManagementService, SpecialistManagementService>();
            services.AddScoped<IRequestManagementService, RequestManagementService>();
            services.AddScoped<IRequestInfoService, RequestManagementService>();




            services.AddSingleton<IMap<Specialist, SpecialistDTO>, SpecialistMapper>();
            services.AddSingleton<IMap<Message, MessageDTO>, MessageMapper>();
            services.AddSingleton<IMap<Request, RequestDTO>, RequestMapper>();
           // services.AddSingleton<IMap<Specialist, SpecialistDTO>, SpecialistMapper>();
            services.AddSingleton<IMapTo<Specialist, SpecialistSlim>, SpecialistSlimMapper>();
            return services;
        }
    }

}
