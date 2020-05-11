using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class SpecialistViewModel
    {
        [Display(Name = "#")]
        public int Id { get; set; }
        [Display(Name = "Ім'я")]
        public string Name { get; set; }
        [Display(Name = "Прізвище")]
        public string Surname { get; set; }
        [Display(Name = "Спеціаліст")]
        public string FullName { get => Name + " " + Surname; }
        [Display(Name = "Оброблені заявки")]
        public int NumberOfProcessedRequests { get; set; }
        [Display(Name = "Коротка інформація")]
        public string ActiveRequestsInformation { get; set; }

        public override string ToString()
        {
            return Name + " " + Surname;
        }
    }
}
