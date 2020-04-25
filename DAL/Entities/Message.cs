using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;

namespace DAL.Entities
{
    public class Message : IHasId
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime ApplicationDate { get; set; }
    }
}
