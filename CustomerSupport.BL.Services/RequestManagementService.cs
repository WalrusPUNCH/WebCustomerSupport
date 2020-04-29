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
        public int CreateRequest(RequestDTO request)
        {
            request.ApplicationDate = DateTime.Now;
            request.Messages.First().ApplicationDate = DateTime.Now;

            SpecialistDTO spec = mapper.Map<SpecialistDTO>(unitOfWork.Specialists.GetTheLeastBusySpecialist());
            if (spec != null)
            {
                request.Status = StatusModel.Processing;
                request.Specialist = spec;
                spec.ActiveRequests.Add(request);
            }

            unitOfWork.Specialists.Update(mapper.Map<Specialist>(spec));
            unitOfWork.Save();
            request.Id = unitOfWork.Requests.GetAll().Last().Id;
            return request.Id;
        }

        public void Delete(int id)
        {
            unitOfWork.Requests.Delete(id);
            unitOfWork.Save();
        }

        public IEnumerable<RequestDTO> GetAll()
        {
            return mapper.Map<IEnumerable<RequestDTO>>(unitOfWork.Requests.GetAll());
        }

        public RequestDTO GetById(int id)
        {
            return mapper.Map<RequestDTO>(unitOfWork.Requests.GetById(id));
        }

        public void Update(RequestDTO request)
        {
            if (request.Specialist != null)
            {
                request.Specialist = mapper.Map<SpecialistDTO>(unitOfWork.Specialists.GetByIdSlim(request.Specialist.Id));
                if (request.Status == StatusModel.Processed)
                    request.Specialist.NumberOfProcessedRequests++;
            }

           
            unitOfWork.Requests.Update(mapper.Map<Request>(request));
            unitOfWork.Save();          
        }
    }
}
