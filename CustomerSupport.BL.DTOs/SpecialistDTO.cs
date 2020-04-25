using System;
using System.Collections.Generic;

namespace CustomerSupport.BL.DTOs
{
    public class SpecialistDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int NumberOfProcessedRequests { get; set; }
        public ICollection<RequestDTO> ActiveRequests { get; set; }
    }
}
