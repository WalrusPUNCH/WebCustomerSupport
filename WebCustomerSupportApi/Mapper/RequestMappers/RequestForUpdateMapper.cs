using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using WebCustomerSupportApi.Mapper.Abstract;
using WebCustomerSupportApi.Models;
using CustomerSupport.BL.DTOs;

namespace WebCustomerSupportApi.Mapper
{
    public class RequestForUpdateMapper : IMapFrom<RequestDTO, RequestForUpdateModel>
    {
        public RequestDTO MapFrom(RequestForUpdateModel model)
        {
            RequestDTO requestDTO = new RequestDTO()
            {
                Id = model.Id,
                ApplicationDate = DateTime.Now,
                SpecialistId = model.SpecialistId,
                Status = (StatusModel)model.Status,
            };
            return requestDTO;
        }
    }
}
