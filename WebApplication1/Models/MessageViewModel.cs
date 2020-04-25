using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class MessageViewModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime ApplicationDate { get; set; }
    }
}
