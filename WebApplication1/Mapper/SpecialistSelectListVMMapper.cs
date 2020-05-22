using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CustomerSupport.BL.DTOs;
using WebApplication1.Models;
using WebApplication1.Web.Mapper.Abstract;

namespace WebApplication1.Web.Mapper
{
    public class SpecialistSelectListVMMapper : IMapTo<SpecialistDTO, SpecialistSelectListViewModel>
    {
        public SpecialistSelectListViewModel MapTo(SpecialistDTO specialistDTO) 
        {
            SpecialistSelectListViewModel specialistVM = new SpecialistSelectListViewModel()
            {
                Id = specialistDTO.Id,
                FullName = specialistDTO.Name + " " + specialistDTO.Surname
            };
            return specialistVM;
        }
    }
}
