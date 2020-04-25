using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

using CustomerSupport.BL.DTOs;
using WebApplication1.Models;

namespace WebApplication1.Mapper
{
    public class PLMapperProfile : Profile
    {
        public PLMapperProfile()
        {
            CreateMap<SpecialistDTO, SpecialistViewModel>().ForMember(dest => dest.ActiveRequestsInformation,
                model => model.MapFrom(src => string.Join("\n", src.ActiveRequests.Select(req => $"{req.Id}: {req.Subject}".Trim())))).ReverseMap();
            CreateMap<MessageViewModel, MessageDTO>().ReverseMap(); 
            CreateMap<RequestDetailsViewModel, RequestDTO>().ReverseMap().ForMember(dest => dest.Specialist,
                model => model.MapFrom(src => src.Specialist.Name + " " + src.Specialist.Surname));
            CreateMap<RequestViewModel, RequestDTO>().ForMember(dest => dest.Messages,
                VM => VM.MapFrom<MessageMappResolver>()).ReverseMap();
        }
    }
}
