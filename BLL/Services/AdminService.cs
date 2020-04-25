using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;


using CustomerSupport.DAL.Abstract;
using CustomerSupport.DAL.Entities;
using BLL.Interfaces;
using BLL.Models;

namespace BLL.Services
{
    public class SpecialistsManagementService : ISpecialistManagementService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public SpecialistsManagementService(IUnitOfWork unit, IMapper mapper)
        {
            unitOfWork = unit;
            this.mapper = mapper;
        }

        public bool DeleteById(int id)
        {
            var specialist = unitOfWork.SpecialistsRepository.GetById(id);
            foreach (var request in unitOfWork.RequestsRepository.GetAll())
            {
                if (request.Specialist.Id == specialist.Id)
                {
                    request.Specialist = null;
                    request.Status = Status.Queued;
                    unitOfWork.RequestsRepository.Update(mapper.Map<Request>(request));
                }
            }
            return unitOfWork.SpecialistsRepository.Delete(id);
        }

        public SpecialistModel GetSpecialistById(int id)
        {
            return mapper.Map<SpecialistModel>(unitOfWork.SpecialistsRepository.GetById(id));
        }

        public IEnumerable<SpecialistModel> GetSpecialists()
        {
            return mapper.Map<IEnumerable<SpecialistModel>>(unitOfWork.SpecialistsRepository.GetAll());        
        }


        public void AddNewSpecialist(SpecialistModel item)
        {
            unitOfWork.SpecialistsRepository.Insert(mapper.Map<Specialist>(item));
        }

        public void Update(SpecialistModel item)
        {
            unitOfWork.SpecialistsRepository.Update(mapper.Map<Specialist>(item));
        }
    }
}
