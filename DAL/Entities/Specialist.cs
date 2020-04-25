using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;

namespace DAL.Entities
{
    public class Specialist : IHasId
    {
        public int Id { get; set; }
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
