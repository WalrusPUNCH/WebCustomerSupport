using System;
using System.Collections.Generic;


namespace CustomerSupport.DAL.Entities
{
    public class Specialist : HasID
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int NumberOfProcessedRequests { get; set; }
        public ICollection<Request> ActiveRequests { get; set; }

        public Specialist()
        {
            ActiveRequests = new List<Request>();
        }
    }
}
