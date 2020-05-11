using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using WebApplication1.Mapper.Abstract;
using CustomerSupport.BL.DTOs;
using WebApplication1.Models;

using CustomerSupport.Core.Mapper;

namespace WebApplication1.Mapper
{
    public class PLMapper : CustomMapperCore, IPLMapper
    {
      //  private readonly Dictionary<KeyValuePair<Type, Type>, Delegate> mappingConfig = new Dictionary<KeyValuePair<Type, Type>, Delegate>();

        public PLMapper()
        {
            /// BL-PL Message mappers
            RegisterMapping<MessageDTO, MessageViewModel>(Map_MessageDTO_MessageViewModel);
            /// PL-BL Message mappers
            RegisterMapping<MessageViewModel, MessageDTO>(Map_MessageViewModel_MessageDTO);


            /// BL-PL Specialist mappers
            RegisterMapping<SpecialistDTO, SpecialistSelectListViewModel>(Map_SpecialistDTO_SpecialistSelectListViewModel);
            RegisterMapping<SpecialistDTO, SpecialistViewModel>(Map_SpecialistDTO_SpecialistViewModel);
            RegisterMapping<SpecialistSlim, SpecialistViewModel>(Map_SpecialistSlim_SpecialistViewModel);

            /// PL-BL Specialist mappers
            RegisterMapping<SpecialistViewModel, SpecialistDTO>(Map_SpecialistViewModel_SpecialistDTO);
            RegisterMapping<SpecialistViewModel, SpecialistSlim>(Map_SpecialistViewModel_SpecialistSlim);

            /// BL-PL Requests mappers
            RegisterMapping<RequestDTO, RequestDetailsViewModel>(Map_RequestDTO_RequestDetailsViewModel);
            RegisterMapping<RequestDTO, RequestEditViewModel>(Map_RequestDTO_RequestEditViewModel);
            /// PL-BL Requests mappers
            RegisterMapping<RequestEditViewModel, RequestDTO>(Map_RequestDetailsViewModel_RequestDTO);
        }
        
        #region BL-PL Message Mappers
        private MessageViewModel Map_MessageDTO_MessageViewModel(MessageDTO sourceItem)
        {
            return new MessageViewModel()
            {
                Id = sourceItem.Id,
                ApplicationDate = sourceItem.ApplicationDate,
                Text = sourceItem.Text
            };
        }
        #endregion
        #region PL-BL Message Mappers
        private MessageDTO Map_MessageViewModel_MessageDTO(MessageViewModel sourceItem)
        {
            return new MessageDTO()
            {
                Id = sourceItem.Id,
                ApplicationDate = sourceItem.ApplicationDate,
                Text = sourceItem.Text
            };
        }
        #endregion

        #region BL-PL Specialist Mappers
        private SpecialistSelectListViewModel Map_SpecialistDTO_SpecialistSelectListViewModel(SpecialistDTO sourceItem)
        {
            return new SpecialistSelectListViewModel()
            {
                Id = sourceItem.Id,
                FullName = sourceItem.Name + " " + sourceItem.Surname
            };
        }

        private SpecialistViewModel Map_SpecialistDTO_SpecialistViewModel(SpecialistDTO sourceItem)
        {
            string requestsInformation = string.Join("\n", sourceItem.ActiveRequests.Select(req =>
            {
                if (req.Status == StatusModel.Processed)
                    return $"[+]{req.Id}: {req.Subject}".Trim();
                else
                    return
                    $"{req.Id}: {req.Subject}".Trim();
            }));

            return new SpecialistViewModel()
            {
                Id = sourceItem.Id,
                Name = sourceItem.Name,
                Surname = sourceItem.Surname,
                ActiveRequestsInformation = requestsInformation,
                NumberOfProcessedRequests = sourceItem.NumberOfProcessedRequests
            };
        }

        private SpecialistViewModel Map_SpecialistSlim_SpecialistViewModel(SpecialistSlim sourceItem)
        {
            SpecialistViewModel specialistView = new SpecialistViewModel()
            {
                Id = sourceItem.Id,
                Name = sourceItem.Name,
                Surname = sourceItem.Surname,
                NumberOfProcessedRequests = sourceItem.NumberOfProcessedRequests
            };
            return specialistView;
        }
        #endregion
        #region PL-BL Specialist Mappers
        private SpecialistDTO Map_SpecialistViewModel_SpecialistDTO(SpecialistViewModel sourceItem)
        {
            return new SpecialistDTO()
            {
                Id = sourceItem.Id,
                ActiveRequests = null,
                Name = sourceItem.Name,
                Surname = sourceItem.Surname,
                NumberOfProcessedRequests = sourceItem.NumberOfProcessedRequests
            };
        }

        private SpecialistSlim Map_SpecialistViewModel_SpecialistSlim(SpecialistViewModel sourceItem)
        {
            SpecialistSlim specialist = new SpecialistSlim()
            {
                Id = sourceItem.Id,
                Name = sourceItem.Name,
                Surname = sourceItem.Surname,
                NumberOfProcessedRequests = sourceItem.NumberOfProcessedRequests
            };
            return specialist;
        }
        #endregion

        #region BL-PL Requests Mappers
        private RequestDetailsViewModel Map_RequestDTO_RequestDetailsViewModel(RequestDTO sourceItem)
        {
            RequestDetailsViewModel requestDetails = new RequestDetailsViewModel()
            {
                Id = sourceItem.Id,
                ApplicationDate = sourceItem.ApplicationDate,
                Messages = MapMany<MessageViewModel>(sourceItem.Messages).ToList(),
                Status = (StatusEnum)sourceItem.Status,
                Subject = sourceItem.Subject
            };

            if (sourceItem.Specialist != null)
                requestDetails.SpecialistFullName = sourceItem.Specialist.Name + " " + sourceItem.Specialist.Surname;

            return requestDetails;
        }

        private RequestEditViewModel Map_RequestDTO_RequestEditViewModel(RequestDTO sourceItem)
        {
            return new RequestEditViewModel()
            {
                Id = sourceItem.Id,
                ApplicationDate = sourceItem.ApplicationDate,
                //Specialist = Map<SpecialistViewModel>(sourceItem.Specialist),
                AvailableSpecialists = new List<SpecialistViewModel>(),
                Status = (StatusEnum)sourceItem.Status,
                Subject = sourceItem.Subject
            };
        }

        #endregion
        #region PL-BL Requests Mappers

        private RequestDTO Map_RequestDetailsViewModel_RequestDTO(RequestEditViewModel sourceItem)
        {
            RequestDTO request = new RequestDTO()
            {
                ApplicationDate = sourceItem.ApplicationDate,
                Id = sourceItem.Id,
                Messages = null,
                Specialist = MapOne<SpecialistSlim>(sourceItem.Specialist),
                Status = (StatusModel)sourceItem.Status,
                Subject = sourceItem.Subject
            };
            return request;
        }
        #endregion
    }
}
