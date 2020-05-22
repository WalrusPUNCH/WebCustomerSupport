using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

using CustomerSupport.BL.DTOs;
using WebApplication1.Models;
using WebApplication1.Web.Mapper.Abstract;
using WebApplication1.Web.Mapper;

namespace WebApplication1.Mapper
{
    public static class PLMapperConfiguration
    {
        public static IServiceCollection ConfigurePLMapper(this IServiceCollection services)
        {
            services.AddSingleton<IMap<MessageDTO, MessageViewModel>, MessageMapper>();
            services.AddSingleton<IMapTo<RequestDTO, RequestDetailsViewModel>, RequestDetailsVMMapper> ();
            services.AddSingleton<IMap<RequestDTO, RequestEditViewModel>, RequestEditVMMapper>();
            services.AddSingleton<IMap<SpecialistDTO, SpecialistViewModel>, SpecialistMapper>();
            services.AddSingleton<IMapTo<SpecialistDTO, SpecialistSelectListViewModel>, SpecialistSelectListVMMapper>();
            services.AddSingleton<IMap<SpecialistSlim, SpecialistViewModel>, SpecialistSlimMapper>();
            return services;
        }
    }

}
