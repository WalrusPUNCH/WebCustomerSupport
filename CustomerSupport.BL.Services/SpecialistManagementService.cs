using System;
using System.Collections.Generic;


using AutoMapper;


using CustomerSupport.DAL.Abstract;
using CustomerSupport.DAL.Entities;
using CustomerSupport.BL.DTOs;
using CustomerSupport.BL.Abstract;
using System.Linq;
using CustomerSupport.BL.Services.Mapper.Abstract;

namespace CustomerSupport.BL.Services
{
    public class SpecialistManagementService : ISpecialistManagementService
    {
        private readonly IUnitOfWork unitOfWork;
        // private readonly IMapper mapper;
        private readonly IBLMapper customMapper;
        public SpecialistManagementService(IUnitOfWork unit,
                                           /*IMapper mapper,*/
                                           IBLMapper customMapper)
        {
            unitOfWork = unit;
            this.customMapper = customMapper;
          //  this.mapper = mapper;
        }
        public void AddSpecialist(SpecialistDTO item)
        {
            unitOfWork.Specialists.Add(customMapper.MapOne<Specialist>(item));
            unitOfWork.Save();

        }

        public SpecialistDTO GetSpecialistById(int id)
        {
            return customMapper.MapOne<SpecialistDTO>(unitOfWork.Specialists.FindByID(id));
        }

        public IEnumerable<SpecialistDTO> GetAll()
        {
            return customMapper.MapMany<SpecialistDTO>(unitOfWork.Specialists.GetAll());
        }

        public IEnumerable<SpecialistDTO> GetAll(int page, int pageSize)
        {
            return customMapper.MapMany<SpecialistDTO>(unitOfWork.Specialists.GetAll(page, pageSize));        
        }

        public void Update(SpecialistDTO item)
        {
            unitOfWork.Specialists.Update(customMapper.MapOne<Specialist>(item));
            unitOfWork.Save();
        }
        public void Delete(int id)
        {
            unitOfWork.Specialists.Delete(id);
            unitOfWork.Save();
        }
        public int Count()
        {
            return unitOfWork.Specialists.Count();
        }
        public IEnumerable<SpecialistDTO> GetSpecialistsWithAmountOfRequestsAboveAvarage()
        {
            return customMapper.MapMany<SpecialistDTO>(unitOfWork.Specialists.GetSpecialistsWithAmountOfRequestsAboveAvarage());
        }

        public IEnumerable<SpecialistDTO> GetSpecialistsWithNoActiveRequests()
        {
            return customMapper.MapMany<SpecialistDTO>(unitOfWork.Specialists.GetSpecialistsWithNoActiveRequests());
        }
    }
}
