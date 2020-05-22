using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebCustomerSupportApi.Models 
{ 
    public class RequestForUpdateModel
    {
        [ReadOnly(true)]
        public int Id { get; set; }
        [EnumDataType(typeof(StatusEnum))]
        public StatusEnum Status { get; set; }

        public int? SpecialistId { get; set; }
    }
}
