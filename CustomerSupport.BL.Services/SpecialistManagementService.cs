using System;
using System.Collections.Generic;
using System.Linq;

using CustomerSupport.DAL.Abstract;
using CustomerSupport.DAL.Entities;
using CustomerSupport.DAL.Specifications;
using CustomerSupport.BL.DTOs;
using CustomerSupport.BL.Abstract;
using CustomerSupport.BL.Abstract.Mapper;

namespace CustomerSupport.BL.Services
{
    public class SpecialistManagementService : ISpecialistManagementService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMap<Specialist, SpecialistDTO> specialistMapper;
        public SpecialistManagementService(IUnitOfWork unit,
                                            IMap<Specialist, SpecialistDTO> specialistMapper)
        {
            unitOfWork = unit;
            this.specialistMapper = specialistMapper;
        }
        public int AddSpecialist(SpecialistDTO item)
        {
            Specialist specialist = specialistMapper.MapFrom(item);
            unitOfWork.Specialists.Add(specialist);
            unitOfWork.Save();
            return specialist.Id;

        }

        public SpecialistDTO GetById(int id)
        {
            Specialist foundSpecialist = unitOfWork.Specialists.FindByID(id);
            if (foundSpecialist == null)
                return null;
            return specialistMapper.MapTo(foundSpecialist);
            // return customMapper.MapOne<SpecialistDTO>(unitOfWork.Specialists.FindByID(id));

        }

        public IEnumerable<SpecialistDTO> GetAll()
        {
            // return customMapper.MapMany<SpecialistDTO>(unitOfWork.Specialists.GetAll());
            return unitOfWork.Specialists.GetAll().Select(s => specialistMapper.MapTo(s));
        }

        public IEnumerable<SpecialistDTO> GetAll(int page, int pageSize)
        {
            //return customMapper.MapMany<SpecialistDTO>(unitOfWork.Specialists.GetAll(page, pageSize));     
            return unitOfWork.Specialists.GetAll(page, pageSize).Select(s => specialistMapper.MapTo(s));
        }

        public void Update(SpecialistDTO item)
        {
            // unitOfWork.Specialists.Update(customMapper.MapOne<Specialist>(item));
            Specialist specialist = unitOfWork.Specialists.FindByID(item.Id);
            specialist.Name = item.Name;
            specialist.Surname = item.Surname;
            unitOfWork.Specialists.Update(specialist);
            //unitOfWork.Specialists.Update(specialistMapper.MapFrom(item));
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

            //return customMapper.MapMany<SpecialistDTO>(unitOfWork.Specialists.GetSpecialistsWithAmountOfRequestsAboveAvarage());
            return unitOfWork.Specialists.GetSpecialistsWithAmountOfRequestsAboveAvarage().Select(s => specialistMapper.MapTo(s));
        }

        public IEnumerable<SpecialistDTO> GetSpecialistsWithNoActiveRequests()
        {
            return unitOfWork.Specialists.GetFiltered(new SpecialistsWithNoActiveRequestsSpecification()).Select(s => specialistMapper.MapTo(s));

            //return customMapper.MapMany<SpecialistDTO>(unitOfWork.Specialists.GetFiltered(new SpecialistsWithNoActiveRequestsSpecification()));
            // return customMapper.MapMany<SpecialistDTO>(unitOfWork.Specialists.GetSpecialistsWithNoActiveRequests());
        }

        public IEnumerable<SpecialistDTO> GetAllSpecialistsWithRequestsInformation()
        {
            return unitOfWork.Specialists.GetFiltered(new SpecialistsWithRequestsInfoSpecification()).Select(s => specialistMapper.MapTo(s));
        }
    }
}
