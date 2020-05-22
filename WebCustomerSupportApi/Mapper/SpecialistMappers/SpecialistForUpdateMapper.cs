using CustomerSupport.BL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCustomerSupportApi.Mapper.Abstract;
using WebCustomerSupportApi.Models;

namespace WebCustomerSupportApi.Mapper
{
    public class SpecialistForUpdateMapper : IMapFrom<SpecialistDTO, SpecialistForUpdateModel>
    {
        public SpecialistDTO MapFrom(SpecialistForUpdateModel model)
        {
            SpecialistDTO specialistDTO = new SpecialistDTO()
            {
                Id = model.Id,
                Name = model.Name,
                Surname = model.Surname
            };
            return specialistDTO;
        }
    }
}
