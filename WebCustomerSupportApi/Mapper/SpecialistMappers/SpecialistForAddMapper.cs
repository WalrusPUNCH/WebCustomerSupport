using CustomerSupport.BL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCustomerSupportApi.Mapper.Abstract;
using WebCustomerSupportApi.Models;

namespace WebCustomerSupportApi.Mapper
{
    public class SpecialistForAddMapper : IMapFrom<SpecialistDTO, SpecialistForAddModel>
    {
        public SpecialistDTO MapFrom(SpecialistForAddModel model)
        {
            SpecialistDTO specialistDTO = new SpecialistDTO()
            {
                Name = model.Name,
                Surname = model.Surname,
                NumberOfProcessedRequests = 0
            };
            return specialistDTO;
        }
    }
}
