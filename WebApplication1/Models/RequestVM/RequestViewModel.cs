using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;


namespace WebApplication1.Models
{
    public class RequestViewModel
    {
        [Display(Name = "Тема звернення")]
        public string Subject { get; set; }
        [Display(Name = "Опишіть проблему")]
        public MessageViewModel InitMessage { get; set; }
    }
}
