using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

using DAL.Interfaces;
using DAL.Entities;
using BLL.Models;
using BLL.Interfaces;

namespace BLL.Services
{
    public class RequestManagerService : IRequestService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public RequestManagerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        public void CreateRequest(RequestModel request)
        {
            request.Specialist = mapper.Map<SpecialistModel>(unitOfWork.SpecialistsRepository.GetTheLeastBusySpecialist());
            request.ApplicationDate = DateTime.Now;
            request.Messages.First().ApplicationDate = DateTime.Now;
            request.Status = StatusModel.Processing;
            int requestID = unitOfWork.RequestsRepository.Insert(mapper.Map<Request>(request));
            request.Id = requestID;
            request.Specialist.ActiveRequests.Add(request);
            unitOfWork.SpecialistsRepository.Update(mapper.Map<Specialist>(request.Specialist));
            
        }

        public IEnumerable<RequestModel> GetAll()
        {
            return mapper.Map<IEnumerable<RequestModel>>(unitOfWork.RequestsRepository.GetAll());
        }

        public RequestModel GetById(int id)
        {
            return mapper.Map<RequestModel>(unitOfWork.RequestsRepository.GetById(id));
        }
    }
}
