using System;
using System.Collections.Generic;
using System.Text;

using CustomerSupport.BL.DTOs;
using CustomerSupport.DAL.Entities;
using CustomerSupport.BL.Abstract.Mapper;

namespace CustomerSupport.BL.Services.Mapper
{
    public class MessageMapper : IMap<Message, MessageDTO>
    {
        public Message MapFrom(MessageDTO messageDTO)
        {
            Message message = new Message()
            {
                Id = messageDTO.Id,
                ApplicationDate = messageDTO.ApplicationDate,
                RequestId = messageDTO.RequestId,
                Text = messageDTO.Text
            };
            return message;
        }

        public MessageDTO MapTo(Message message)
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
    }
}
