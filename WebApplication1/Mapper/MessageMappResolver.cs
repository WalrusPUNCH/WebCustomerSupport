using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

using CustomerSupport.BL.DTOs;
using WebApplication1.Models;

namespace WebApplication1.Mapper
{
    public class MessageMappResolver : AutoMapper.IValueResolver<RequestViewModel, RequestDTO, ICollection<MessageDTO>>
    {
        public ICollection<MessageDTO> Resolve(RequestViewModel source, RequestDTO destination, ICollection<MessageDTO> destMember, ResolutionContext context)
        {
            return new List<MessageDTO>() { new MessageDTO() { Text = source.InitMessage.Text } };
        }
    }
}
