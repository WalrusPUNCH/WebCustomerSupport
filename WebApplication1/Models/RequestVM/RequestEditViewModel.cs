using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class RequestEditViewModel
    {
        [Display(Name = "Номер")]
        public int Id { get; set; }
        [Display(Name = "Тема звернення")]
        public string Subject { get; set; }
        [Display(Name = "Час прийому заявки")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:f}")]
        public DateTime ApplicationDate { get; set; }
        [Display(Name = "Статус заявки")]
        public StatusEnum Status { get; set; }
        [Display(Name = "Відповідальний спеціаліст")]
       // public SpecialistViewModel Specialist { get; set; }   
        public int? SpecialistId { get; set; }
        public IEnumerable<SpecialistViewModel> AvailableSpecialists { get; set; }
    }
}
