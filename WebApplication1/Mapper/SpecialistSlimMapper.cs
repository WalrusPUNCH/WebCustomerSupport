using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CustomerSupport.BL.DTOs;

using WebApplication1.Models;
using WebApplication1.Web.Mapper.Abstract;

namespace WebApplication1.Web.Mapper
{
    public class SpecialistSlimMapper : IMap<SpecialistSlim, SpecialistViewModel>
    {
        public SpecialistSlim MapFrom(SpecialistViewModel specialistVM)
        {
            SpecialistSlim specialist = new SpecialistSlim()
            {
                Id = specialistVM.Id,
                Name = specialistVM.Name,
                Surname = specialistVM.Surname,
                NumberOfProcessedRequests = specialistVM.NumberOfProcessedRequests
            };
            return specialist;
        }

        public SpecialistViewModel MapTo(SpecialistSlim specialistSlim)
        {
            SpecialistViewModel specialistView = new SpecialistViewModel()
            {
                Id = specialistSlim.Id,
                Name = specialistSlim.Name,
                Surname = specialistSlim.Surname,
                NumberOfProcessedRequests = specialistSlim.NumberOfProcessedRequests
            };
            return specialistView;
        }
    }
}
