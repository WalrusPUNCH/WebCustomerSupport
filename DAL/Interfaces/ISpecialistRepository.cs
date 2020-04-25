using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Interfaces
{
    interface ISpecialistRepository : IBaseRepository<Specialist>
    {
        Specialist GetSpecialistByName(string name);
        IEnumerable<Specialist> GetSpecialistsWithAboveAvarageAmountOfRequests();
        IEnumerable<Specialist> GetSpecialistsWithNoActiveRequests();
        Specialist GetTheLeastBusySpecialist();
    }
}
