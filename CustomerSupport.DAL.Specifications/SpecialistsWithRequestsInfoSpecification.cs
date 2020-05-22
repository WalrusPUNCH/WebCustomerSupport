using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using CustomerSupport.DAL.Specifications.Abstract;
using CustomerSupport.DAL.Entities;
using System.Linq.Expressions;

namespace CustomerSupport.DAL.Specifications
{
    public class SpecialistsWithRequestsInfoSpecification : BaseSpecification<Specialist>
    {
        public SpecialistsWithRequestsInfoSpecification()
            : base(spec => spec.Id != -1)
        {
            AddInclude(s => s.ActiveRequests);
        }
    }
}
