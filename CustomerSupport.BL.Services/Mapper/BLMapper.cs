using System;
using System.Collections.Generic;
using System.Text;


using CustomerSupport.Core.Mapper;
using CustomerSupport.BL.Services.Mapper.Abstract;
using CustomerSupport.DAL.Entities;
using CustomerSupport.BL.DTOs;
using System.Linq;

namespace CustomerSupport.BL.Services.Mapper
{
    public class BLMapper : CustomMapperCore, IBLMapper
    {
        public BLMapper()
        {
            /// BL-DAL Message mappers

            /// DAL-BL Message mappers
            RegisterMapping<Message, MessageDTO>(Map_Message_MessageDTO);

            /// BL-DAL Specialist mappers
            RegisterMapping<SpecialistDTO, Specialist>(Map_SpecialistDTO_Specialist);
            /// DAL-BL Specialist mappers
            RegisterMapping<Specialist, SpecialistDTO>(Map_Specialist_SpecialistDTO);
            RegisterMapping<Specialist, SpecialistSlim>(Map_Specialist_SpecialistSlim);

            /// BL-DAL Requests mappers
            RegisterMapping<RequestDTO, Request>(Map_RequestDTO_Request);
            /// DAL-BL Requests mappers
            RegisterMapping<Request, RequestDTO>(Map_Request_RequestDTO);
        }

        #region DAL-BL Message Mappers
        private MessageDTO Map_Message_MessageDTO(Message message)
        {
            MessageDTO messageDTO = new MessageDTO()
            {
                ApplicationDate = message.ApplicationDate,
                Id = message.Id,
                Text = message.Text,
                RequestId = message.RequestId
            };
            return messageDTO;
        }
        #endregion
        #region BL-DAL Message Mappers
        #endregion

        #region DAL-BL Request Mappers
        private RequestDTO Map_Request_RequestDTO(Request request)
        {
            RequestDTO requestDTO = new RequestDTO()
            {
                ApplicationDate = request.ApplicationDate,
                Id = request.Id,
                Messages = MapMany<MessageDTO>(request.Messages).ToList(),
                Status = (StatusModel)request.Status,
                Subject = request.Subject
            };
            if (request.Specialist != null)
                requestDTO.Specialist = MapOne<SpecialistSlim>(request.Specialist);

            return requestDTO;
        }
        #endregion
        #region BL-DAL Request Mappers
        private Request Map_RequestDTO_Request(RequestDTO requestDTO)
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
                request.Messages = MapMany<Message>(requestDTO.Messages).ToList();

            return request;
        }
        #endregion

        #region DAL-BL Specialist Mappers
        private SpecialistDTO Map_Specialist_SpecialistDTO(Specialist specialist)
        {
            SpecialistDTO specialistDTO = new SpecialistDTO()
            {
                Id = specialist.Id,
                ActiveRequests = MapMany<RequestDTO>(specialist.ActiveRequests).ToList(),
                Name = specialist.Name,
                Surname = specialist.Surname,
                NumberOfProcessedRequests = specialist.NumberOfProcessedRequests
            };
            return specialistDTO;
        }

        private SpecialistSlim Map_Specialist_SpecialistSlim(Specialist specialist)
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
        #endregion
        #region BL-DAL Specialist Mappers
        private Specialist Map_SpecialistDTO_Specialist(SpecialistDTO specialistDTO)
        {
            Specialist specialist = new Specialist()
            {
                Id = specialistDTO.Id,
                Name = specialistDTO.Name,
                Surname = specialistDTO.Surname,
                NumberOfProcessedRequests = specialistDTO.NumberOfProcessedRequests
            };
            if (specialistDTO.ActiveRequests != null && specialistDTO.ActiveRequests.Count != 0)
                specialist.ActiveRequests = MapMany<Request>(specialistDTO.ActiveRequests).ToList();
            return specialist;
        }
        #endregion
    }
}
