using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using CustomerSupport.BL.Abstract;
using CustomerSupport.BL.Abstract.Mapper;
using CustomerSupport.BL.DTOs;
using CustomerSupport.BL.Services;
using CustomerSupport.BL.Services.Mapper;
using CustomerSupport.DAL.Abstract;
using CustomerSupport.DAL.Entities;
using CustomerSupport.DAL.Impl;
using Microsoft.EntityFrameworkCore;
using WebCustomerSupportApi.Mapper;
using WebCustomerSupportApi.Models;

namespace WebCustomerSupportApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<CustomerSupportContext>(options => options.UseSqlServer(connection));

            services.AddSingleton<IMap<Specialist, SpecialistDTO>, SpecialistMapper>();
            services.AddSingleton<IMap<Message, MessageDTO>, MessageMapper>();
            services.AddSingleton<IMap<Request, RequestDTO>, RequestMapper>();
            services.AddSingleton<IMap<Specialist, SpecialistDTO>, SpecialistMapper>();
            services.AddSingleton<IMapTo<Specialist, SpecialistSlim>, SpecialistSlimMapper>();

            services.AddSingleton<Mapper.Abstract.IMapFrom<SpecialistDTO, SpecialistForUpdateModel>, SpecialistForUpdateMapper>();
            services.AddSingleton<Mapper.Abstract.IMapFrom<SpecialistDTO, SpecialistForAddModel>, SpecialistForAddMapper>();

            services.AddSingleton<Mapper.Abstract.IMapFrom<RequestDTO, RequestForUpdateModel>, RequestForUpdateMapper>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ISpecialistManagementService, SpecialistManagementService>();
            services.AddScoped<IRequestManagementService, RequestManagementService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
