using System;


namespace CustomerSupport.BL.DTOs
{
    public class MessageDTO
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime ApplicationDate { get; set; }
        public int RequestId { get; set; }
    }
}
