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
                unitOfWork.Specialists.GetById(spec.Id).ActiveRequests.Add(mapper.Map<Request>(request));
            }

           // request.Specialist.ActiveRequests.Add(request);

            // unitOfWork.RequestsRepository.Insert(mapper.Map<Request>(request));
           // unitOfWork.SpecialistsRepository.Update(mapper.Map<Specialist>(request.Specialist));
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
            RequestDTO DBrequest = mapper.Map<RequestDTO>(unitOfWork.Requests.GetById(request.Id));
            if (DBrequest.Specialist == null && request.Specialist != null)
            {
                SpecialistDTO spec = mapper.Map<SpecialistDTO>(unitOfWork.Specialists.GetById(request.Specialist.Id));
               // spec.ActiveRequests.Add(request);
                unitOfWork.Specialists.GetById(spec.Id).ActiveRequests.Add(mapper.Map<Request>(request));
            }
            else if (request.Specialist == null && DBrequest.Specialist != null)
            {
                SpecialistDTO oldSpec = mapper.Map<SpecialistDTO>(unitOfWork.Specialists.GetById(DBrequest.Specialist.Id));
                unitOfWork.Specialists.GetById(oldSpec.Id).ActiveRequests
                    .Remove(unitOfWork.Specialists.GetById(oldSpec.Id).ActiveRequests.Where(req => req.Id == request.Id).First());
            }
            else if (DBrequest.Specialist.Id == request.Specialist.Id)
            {
                unitOfWork.Requests.GetById(request.Id).Status = mapper.Map<Status>(request.Status);
            }
            else if (DBrequest.Specialist.Id != request.Specialist.Id)
            {
                SpecialistDTO oldSpec = mapper.Map<SpecialistDTO>(unitOfWork.Specialists.GetById(DBrequest.Specialist.Id));
                unitOfWork.Specialists.GetById(oldSpec.Id).ActiveRequests
                    .Remove(unitOfWork.Specialists.GetById(oldSpec.Id).ActiveRequests.Where(req => req.Id == request.Id).First());

                SpecialistDTO newSpec = mapper.Map<SpecialistDTO>(unitOfWork.Specialists.GetById(request.Specialist.Id));
                unitOfWork.Specialists.GetById(newSpec.Id).ActiveRequests
                    .Add(mapper.Map<Request>(request));
            }

            unitOfWork.Save();
            // request.Specialist = spec;
            //unitOfWork.Requests.Update(mapper.Map<Request>(request));
            //unitOfWork.Specialists.Update(mapper.Map<Specialist>(spec));
        }
    }
}
