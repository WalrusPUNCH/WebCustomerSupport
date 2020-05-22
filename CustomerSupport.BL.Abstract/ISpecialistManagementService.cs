using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using CustomerSupport.BL.DTOs;

namespace CustomerSupport.BL.Abstract
{
    public interface ISpecialistManagementService
    {
        int AddSpecialist(SpecialistDTO item);
        IEnumerable<SpecialistDTO> GetAll(int page, int pageSize);
        IEnumerable<SpecialistDTO> GetAll();
        SpecialistDTO GetById(int id);
        void Update(SpecialistDTO item);
        void Delete(int id);
        int Count();

        IEnumerable<SpecialistDTO> GetSpecialistsWithAmountOfRequestsAboveAvarage();
        IEnumerable<SpecialistDTO> GetSpecialistsWithNoActiveRequests();
        IEnumerable<SpecialistDTO> GetAllSpecialistsWithRequestsInformation();
    }
}
