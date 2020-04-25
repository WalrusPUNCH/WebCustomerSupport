using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

using AutoMapper;
using CustomerSupport.BL.Configuration;

namespace WebApplication1.Mapper
{
    public static class GlobalMapperConfiguration
    {
        public static IServiceCollection ConfigureMapper(this IServiceCollection services)
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new BLMapperProfile());  //mapping between Web and Business layer objects
                cfg.AddProfile(new PLMapperProfile());  // mapping between Business and DB layer objects
            });

            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);
            return services;

        }
    }
}
