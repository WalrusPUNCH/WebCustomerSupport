using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

using DAL.Entities;
using BLL.Models;


namespace BLL.Mapper
{
    public class BLMapperProfile : Profile
    {
       public BLMapperProfile()
       {
            CreateMap<Message, MessageModel>().ReverseMap();
            CreateMap<Request, RequestModel>().ReverseMap();
            CreateMap<Specialist, SpecialistModel>().ReverseMap();
            CreateMap<Status, StatusModel>().ReverseMap();
       }
        
    }
}
