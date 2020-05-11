using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

using CustomerSupport.DAL.Entities;
using CustomerSupport.BL.DTOs;
using CustomerSupport.Core.Mapper;



namespace CustomerSupport.BL.Configuration
{
    public class BLMapperProfile : Profile
    {
       public BLMapperProfile()
       {
            CreateMap<Message, MessageDTO>().ReverseMap();
            CreateMap<Request, RequestDTO>().ReverseMap();
            CreateMap<Specialist, SpecialistDTO>().ReverseMap();
            CreateMap<Status, StatusModel>().ReverseMap();

            //CreateMap<Message, MessageDTO>().ForMember(dest => dest.Id, opt => opt.Ignore()).ReverseMap();
            //CreateMap<Request, RequestDTO>().ForMember(dest => dest.Id, opt => opt.Ignore()).ReverseMap();
            //CreateMap<Specialist, SpecialistDTO>().ForMember(dest => dest.Id, opt => opt.Ignore()).ReverseMap();
            //CreateMap<Status, StatusModel>().ReverseMap();
       }

    }
}
