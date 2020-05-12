using System;
using CustomerSupport.DAL.Specifications.Abstract;
using CustomerSupport.DAL.Entities;
using System.Linq.Expressions;
using System.Linq;

namespace CustomerSupport.DAL.Specifications
{
    public class SpecialistsWithNoActiveRequestsSpecification : BaseSpecification<Specialist>
    {
        public SpecialistsWithNoActiveRequestsSpecification()
            : base(spec => spec.ActiveRequests.Where(req => req.Status == Status.Processed).Count() == spec.ActiveRequests.Count)
        {
       
        }
    }
}
