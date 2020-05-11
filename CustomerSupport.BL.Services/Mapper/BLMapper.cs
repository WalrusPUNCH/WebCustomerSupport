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

            /// BL-DAL Requests mappers

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

                             // Specialist = MapOne<SpecialistDTO>(request.Specialist),
                Status = (StatusModel)request.Status,
                Subject = request.Subject
            };
            return requestDTO;
        }
        #endregion
        #region BL-DAL Request Mappers
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
        #endregion
        #region BL-DAL Specialist Mappers
        private Specialist Map_SpecialistDTO_Specialist(SpecialistDTO specialistDTO)
        {
            Specialist specialist = new Specialist()
            {
                Id = specialistDTO.Id,
                ActiveRequests = MapMany<Request>(specialistDTO.ActiveRequests).ToList(),
                Name = specialistDTO.Name,
                Surname = specialistDTO.Surname,
                NumberOfProcessedRequests = specialistDTO.NumberOfProcessedRequests
            };
            return specialist;
        }
        #endregion
    }
}
