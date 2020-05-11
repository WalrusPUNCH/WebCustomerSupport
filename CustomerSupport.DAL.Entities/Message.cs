using System;

namespace CustomerSupport.DAL.Entities
{
    public class Message : HasID
    {
        public string Text { get; set; }
        public DateTime ApplicationDate { get; set; }
        public int RequestId { get; set; }
    }
}
