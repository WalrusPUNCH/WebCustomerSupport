﻿using System;
using System.Collections.Generic;

namespace CustomerSupport.BL.DTOs
{
    public class RequestDTO
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public DateTime ApplicationDate { get; set; }
        public StatusModel Status { get; set; }
        //public int? SpecialistId { get; set; }
        public SpecialistDTO Specialist { get; set; }
        public ICollection<MessageDTO> Messages { get; set; }

        public RequestDTO()
        {
            Messages = new List<MessageDTO>();
        }
    }
}
