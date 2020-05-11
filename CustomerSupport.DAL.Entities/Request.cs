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
        private int? spec;
        public int? SpecialistId
        {
            get { return spec; }
            set
            {
                if (value == null)
                    Status = Status.Queued;
                spec = value;
            }
        }
        public ICollection<Message> Messages { get; set; }
        public Request()
        {
            Messages = new List<Message>();
        }
    }
}
