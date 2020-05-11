using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerSupport.BL.DTOs
{
    public class SpecialistSlim
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int NumberOfProcessedRequests { get; set; }
    }
}
