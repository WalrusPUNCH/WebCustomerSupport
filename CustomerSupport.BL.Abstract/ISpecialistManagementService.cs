using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using CustomerSupport.BL.DTOs;

namespace CustomerSupport.BL.Abstract
{
    public interface ISpecialistManagementService
    {
        void AddSpecialist(SpecialistDTO item);
        IEnumerable<SpecialistDTO> GetAll();
        SpecialistDTO GetSpecialistById(int id);
        void Update(SpecialistDTO item);
        bool Delete(int id);
        IEnumerable<SpecialistDTO> GetSpecialistsWithAmountOfRequestsAboveAvarage();
        IEnumerable<SpecialistDTO> GetSpecialistsWithNoActiveRequests();

    }
}
