using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CustomerSupport.BL.DTOs;
using WebApplication1.Models;
using WebApplication1.Web.Mapper.Abstract;

namespace WebApplication1.Web.Mapper
{
    public class RequestEditVMMapper : IMap<RequestDTO, RequestEditViewModel>
    {
        public RequestDTO MapFrom(RequestEditViewModel sourceItem)
        {
            RequestDTO request = new RequestDTO()
            {
                ApplicationDate = sourceItem.ApplicationDate,
                Id = sourceItem.Id,
                Messages = null,
                SpecialistId = sourceItem.SpecialistId,
                Status = (StatusModel)sourceItem.Status,
                Subject = sourceItem.Subject
            };
            return request;
        }

        public RequestEditViewModel MapTo(RequestDTO sourceItem)
        {
            RequestEditViewModel requestEditView = new RequestEditViewModel()
            {
                Id = sourceItem.Id,
                ApplicationDate = sourceItem.ApplicationDate,
                // Specialist = Map<SpecialistViewModel>(sourceItem.Specialist),
                AvailableSpecialists = new List<SpecialistViewModel>(),
                Status = (StatusEnum)sourceItem.Status,
                Subject = sourceItem.Subject
            };
            if (sourceItem.Specialist != null)
                requestEditView.SpecialistId = sourceItem.Specialist.Id;
            return requestEditView;
        }
    }
}
