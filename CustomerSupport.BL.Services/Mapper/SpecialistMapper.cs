using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CustomerSupport.BL.Abstract.Mapper;
using CustomerSupport.BL.DTOs;
using CustomerSupport.DAL.Entities;

namespace CustomerSupport.BL.Services.Mapper
{
    public class SpecialistMapper : IMap<Specialist, SpecialistDTO>
    {
        private readonly IMap<Request, RequestDTO> requestMapper;
        public SpecialistMapper(IMap<Request, RequestDTO> requestMapper)
        {
            this.requestMapper = requestMapper;
        }
        public Specialist MapFrom(SpecialistDTO specialistDTO)
        {
            Specialist specialist = new Specialist()
            {
                Id = specialistDTO.Id,
                Name = specialistDTO.Name,
                Surname = specialistDTO.Surname,
                NumberOfProcessedRequests = specialistDTO.NumberOfProcessedRequests
            };
            if (specialistDTO.ActiveRequests != null && specialistDTO.ActiveRequests.Count != 0)
                specialist.ActiveRequests = specialistDTO.ActiveRequests.Select(r => requestMapper.MapFrom(r)).ToList();
            return specialist;
        }

        public SpecialistDTO MapTo(Specialist specialist)
        {
            SpecialistDTO specialistDTO = new SpecialistDTO()
            {
                Id = specialist.Id,
                ActiveRequests = specialist.ActiveRequests.Select(r => requestMapper.MapTo(r)).ToList(),
                Name = specialist.Name,
                Surname = specialist.Surname,
                NumberOfProcessedRequests = specialist.NumberOfProcessedRequests
            };
            return specialistDTO;
        }
    }
    
}
