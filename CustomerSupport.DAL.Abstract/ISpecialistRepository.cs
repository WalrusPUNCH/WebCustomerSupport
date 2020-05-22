using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CustomerSupport.DAL.Entities;
using CustomerSupport.DAL.Specifications.Abstract;

namespace CustomerSupport.DAL.Abstract
{
    public interface ISpecialistRepository : IBaseRepository<int, Specialist>
    {
       // IEnumerable<Specialist> GetFiltered(ISpecification<Specialist> filter);
        Specialist GetTheLeastBusySpecialist();
        IEnumerable<Specialist> GetSpecialistsWithAmountOfRequestsAboveAvarage(); // add paging
       // IEnumerable<Specialist> GetSpecialistsWithNoActiveRequests(); // add paging
    }
}
