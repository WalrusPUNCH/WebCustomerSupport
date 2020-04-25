using System;
using System.Collections.Generic;
using System.Linq;


using AutoMapper;

using CustomerSupport.DAL.Abstract;
using CustomerSupport.DAL.Entities;
using CustomerSupport.BL.DTOs;
using CustomerSupport.BL.Abstract;


namespace CustomerSupport.BL.Services
{
    public class RequestManagementService :  IRequestManagementService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public RequestManagementService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        public void CreateRequest(RequestDTO request)
        {
            request.ApplicationDate = DateTime.Now;
            request.Messages.First().ApplicationDate = DateTime.Now;
            request.Status = StatusModel.Processing;

            SpecialistDTO spec = mapper.Map<SpecialistDTO>(unitOfWork.Specialists.GetTheLeastBusySpecialist());
            if (spec != null)
                unitOfWork.Specialists.GetById(spec.Id).ActiveRequests.Add(mapper.Map<Request>(request));

           // request.Specialist.ActiveRequests.Add(request);

            // unitOfWork.RequestsRepository.Insert(mapper.Map<Request>(request));
           // unitOfWork.SpecialistsRepository.Update(mapper.Map<Specialist>(request.Specialist));
            unitOfWork.Save();
            request.Id = unitOfWork.Requests.GetAll().Last().Id;
        }

        public IEnumerable<RequestDTO> GetAll()
        {
            return mapper.Map<IEnumerable<RequestDTO>>(unitOfWork.Requests.GetAll());
        }

        public RequestDTO GetById(int id)
        {
            return mapper.Map<RequestDTO>(unitOfWork.Requests.GetById(id));
        }
    }
}
