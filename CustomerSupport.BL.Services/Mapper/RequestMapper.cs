using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CustomerSupport.BL.Abstract.Mapper;
using CustomerSupport.BL.DTOs;
using CustomerSupport.DAL.Entities;

namespace CustomerSupport.BL.Services.Mapper
{
    public class RequestMapper : IMap<Request, RequestDTO>
    {
        private readonly IMapTo<Specialist, SpecialistSlim> specialistMapper;
        private readonly IMap<Message, MessageDTO> messageMapper;
        public RequestMapper(IMap<Message, MessageDTO> messageMapper, IMapTo<Specialist, SpecialistSlim> specialistMapper)
        {
            this.specialistMapper = specialistMapper;
            this.messageMapper = messageMapper;
        }
        public Request MapFrom(RequestDTO requestDTO)
        {
            Request request = new Request()
            {
                Id = requestDTO.Id,
                ApplicationDate = requestDTO.ApplicationDate,
                SpecialistId = requestDTO.SpecialistId,
                Status = (Status)requestDTO.Status,
                Subject = requestDTO.Subject
            };
            if (requestDTO.Messages != null && request.Messages.Count != 0)
                request.Messages = requestDTO.Messages.Select(m => messageMapper.MapFrom(m)).ToList();

            return request;
        }

        public RequestDTO MapTo(Request request)
        {
            RequestDTO requestDTO = new RequestDTO()
            {
                ApplicationDate = request.ApplicationDate,
                Id = request.Id,
                Messages = request.Messages.Select(m => messageMapper.MapTo(m)).ToList(),
                Status = (StatusModel)request.Status,
                Subject = request.Subject
            };
            if (request.Specialist != null)
                requestDTO.Specialist = specialistMapper.MapTo(request.Specialist);

            return requestDTO;
        }
    }
}
