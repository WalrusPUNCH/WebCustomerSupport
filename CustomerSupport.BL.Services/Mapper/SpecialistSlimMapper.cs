using System;
using System.Collections.Generic;
using System.Text;

using CustomerSupport.BL.Abstract.Mapper;
using CustomerSupport.DAL.Entities;
using CustomerSupport.BL.DTOs;

namespace CustomerSupport.BL.Services.Mapper
{
    public class SpecialistSlimMapper : IMapTo <Specialist, SpecialistSlim>
    {
        public SpecialistSlim MapTo(Specialist specialist)
        {
            SpecialistSlim specialistSlim = new SpecialistSlim()
            {
                Id = specialist.Id,
                Name = specialist.Name,
                Surname = specialist.Surname,
                NumberOfProcessedRequests = specialist.NumberOfProcessedRequests
            };
            return specialistSlim;
        }
    }
}
