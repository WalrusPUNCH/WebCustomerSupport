using System;
using System.Collections.Generic;


namespace CustomerSupport.DAL.Entities
{
    public class Request : HasID
    {
        public string Subject { get; set; }
        public DateTime ApplicationDate { get; set; }
        public Status Status { get; set; }
        public Specialist Specialist { get; set; }
        public ICollection<Message> Messages { get; set; }
    }
}
