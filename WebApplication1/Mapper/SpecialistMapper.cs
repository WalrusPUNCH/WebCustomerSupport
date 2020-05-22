
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CustomerSupport.BL.DTOs;
using WebApplication1.Models;
using WebApplication1.Web.Mapper.Abstract;

namespace WebApplication1.Web.Mapper
{
    public class SpecialistMapper : IMap<SpecialistDTO, SpecialistViewModel>
    {
        public SpecialistDTO MapFrom(SpecialistViewModel specialistVM)
        {
            SpecialistDTO specialistDTO = new SpecialistDTO()
            {
                Id = specialistVM.Id,
                ActiveRequests = null,
                Name = specialistVM.Name,
                Surname = specialistVM.Surname,
                NumberOfProcessedRequests = specialistVM.NumberOfProcessedRequests
            };

            return specialistDTO;
        }

        public SpecialistViewModel MapTo(SpecialistDTO specialistDTO)
        {
            string requestsInformation = string.Join("\n", specialistDTO.ActiveRequests.Select(req =>
            {
                if (req.Status == StatusModel.Processed)
                    return $"[+]{req.Id}: {req.Subject}".Trim();
                else
                    return
                    $"{req.Id}: {req.Subject}".Trim();
            }));

            return new SpecialistViewModel()
            {
                Id = specialistDTO.Id,
                Name = specialistDTO.Name,
                Surname = specialistDTO.Surname,
                ActiveRequestsInformation = requestsInformation,
                NumberOfProcessedRequests = specialistDTO.NumberOfProcessedRequests
            };
        }
    }
}
