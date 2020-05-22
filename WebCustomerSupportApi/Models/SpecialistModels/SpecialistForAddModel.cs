using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebCustomerSupportApi.Models
{
    public class SpecialistForAddModel
    {
        [MinLength(1, ErrorMessage = "Name cannot be empty")]
        [MaxLength(10, ErrorMessage = "The name shouldn't have more than 10 characters.")]
        public string Name { get; set; }

        [MinLength(1, ErrorMessage = "Surname cannot be empty")]
        [MaxLength(10, ErrorMessage = "The surname shouldn't have more than 10 characters.")]
        public string Surname { get; set; }
    }
}
