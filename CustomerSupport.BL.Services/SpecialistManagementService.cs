using System;
using System.Collections.Generic;


using AutoMapper;


using CustomerSupport.DAL.Abstract;
using CustomerSupport.DAL.Entities;
using CustomerSupport.BL.DTOs;
using CustomerSupport.BL.Abstract;
using System.Linq;

namespace CustomerSupport.BL.Services
{
    public class SpecialistManagementService : ISpecialistManagementService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public SpecialistManagementService(IUnitOfWork unit, IMapper mapper)
        {
            unitOfWork = unit;
            this.mapper = mapper;
        }

        public bool Delete(int id)
        {
            bool isDeleted = unitOfWork.Specialists.Delete(id);
            unitOfWork.Save();
            return isDeleted;
        }

        public SpecialistDTO GetSpecialistById(int id)
        {
            return mapper.Map<SpecialistDTO>(unitOfWork.Specialists.GetById(id));
        }

        public IEnumerable<SpecialistDTO> GetAll()
        {
            return mapper.Map<IEnumerable<SpecialistDTO>>(unitOfWork.Specialists.GetAll());        
        }
        
        public void AddSpecialist(SpecialistDTO item)
        {
            unitOfWork.Specialists.Create(mapper.Map<Specialist>(item));
            unitOfWork.Save();

        }

        public void Update(SpecialistDTO item)
        {
            unitOfWork.Specialists.Update(mapper.Map<Specialist>(item));
            unitOfWork.Save();
        }

        public IEnumerable<SpecialistDTO> GetSpecialistsWithAmountOfRequestsAboveAvarage()
        {
            return mapper.Map<IEnumerable<SpecialistDTO>>(unitOfWork.Specialists.GetSpecialistsWithAmountOfRequestsAboveAvarage());
        }

        public IEnumerable<SpecialistDTO> GetSpecialistsWithNoActiveRequests()
        {
            return mapper.Map<IEnumerable<SpecialistDTO>>(unitOfWork.Specialists.GetSpecialistsWithNoActiveRequests());
        }
    }
}
