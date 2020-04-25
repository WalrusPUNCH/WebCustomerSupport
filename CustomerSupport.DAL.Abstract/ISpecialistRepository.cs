using System;
using System.Collections.Generic;

using CustomerSupport.DAL.Entities;

namespace CustomerSupport.DAL.Abstract
{
    public interface ISpecialistRepository : IBaseRepository<Specialist>
    {
        Specialist GetTheLeastBusySpecialist();
        IEnumerable<Specialist> GetSpecialistsWithAmountOfRequestsAboveAvarage();
        IEnumerable<Specialist> GetSpecialistsWithNoActiveRequests();
    }
}
