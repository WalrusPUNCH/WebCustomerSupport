using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CustomerSupport.BL.DTOs;
using WebApplication1.Models;
using WebApplication1.Web.Mapper.Abstract;

namespace WebApplication1.Web.Mapper
{
    public class MessageMapper : IMap<MessageDTO, MessageViewModel>
    {
        public MessageDTO MapFrom(MessageViewModel messageVM)
        {
            MessageDTO messageDTO = new MessageDTO()
            {
                Id = messageVM.Id,
                ApplicationDate = messageVM.ApplicationDate,
                Text = messageVM.Text
            };
            return messageDTO;
        }

        public MessageViewModel MapTo(MessageDTO message)
        {
            MessageViewModel messageVM = new MessageViewModel()
            {
                Id = message.Id,
                ApplicationDate = message.ApplicationDate,
                Text = message.Text
            };
            return messageVM;
        }
    }
}
