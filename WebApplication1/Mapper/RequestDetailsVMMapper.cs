using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CustomerSupport.BL.DTOs;
using WebApplication1.Models;
using WebApplication1.Web.Mapper.Abstract;

namespace WebApplication1.Web.Mapper
{
    public class RequestDetailsVMMapper : IMapTo<RequestDTO, RequestDetailsViewModel>
    {
        private readonly IMap<MessageDTO, MessageViewModel> messageMapper;
        public RequestDetailsVMMapper(IMap<MessageDTO, MessageViewModel> messageMapper)
        {
            this.messageMapper = messageMapper;
        }
        public RequestDetailsViewModel MapTo(RequestDTO requestDTO)
        {
            RequestDetailsViewModel requestDetails = new RequestDetailsViewModel()
            {
                Id = requestDTO.Id,
                ApplicationDate = requestDTO.ApplicationDate,
                Messages = (requestDTO.Messages).Select(m => messageMapper.MapTo(m)).ToList(),
                Status = (StatusEnum)requestDTO.Status,
                Subject = requestDTO.Subject
            };

            if (requestDTO.Specialist != null)
                requestDetails.SpecialistFullName = requestDTO.Specialist.Name + " " + requestDTO.Specialist.Surname;

            return requestDetails;
        }
    }
}
