using System;
using System.Collections.Generic;
using DAL.Interfaces;

namespace DAL.Entities
{
    public class Request :IHasId
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public DateTime ApplicationDate { get; set; }
        public Status Status { get; set; }
        public Specialist Specialist { get; set; }
        public ICollection<Message> Messages { get; set; }
    }
}
