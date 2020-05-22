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
    public class RequestManagementService :  IRequestManagementService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMap<Request, RequestDTO> requestMapper;

        public RequestManagementService(IUnitOfWork unitOfWork,
                                        IMap<Request, RequestDTO> requestMapper)
        {
            this.requestMapper = requestMapper;
            this.unitOfWork = unitOfWork;
        }

        public int CreateRequest(string subject, string initMessage)
        {
            Request request = new Request()
            {
                ApplicationDate = DateTime.Now,
                Subject = subject,
                Messages = new List<Message>() { new Message() { Text = initMessage, ApplicationDate = DateTime.Now } },
                Status = Status.Queued
            };

            Specialist spec = unitOfWork.Specialists.GetTheLeastBusySpecialist();
            if (spec != null)
            {
                request.Status = Status.Processing;
                request.SpecialistId = spec.Id;
                request.Specialist = spec;
            }
            unitOfWork.Requests.Add(request);
            unitOfWork.Save();
            return request.Id;
        }

        public IEnumerable<RequestDTO> GetAll(int page, int pageSize)
        {
            return unitOfWork.Requests.GetAll(page, pageSize).Select(r => requestMapper.MapTo(r));
            //return customMapper.MapMany<RequestDTO>(unitOfWork.Requests.GetAll(page, pageSize));
        }

        public IEnumerable<RequestDTO> GetAll()
        {
            return unitOfWork.Requests.GetAll().Select(r => requestMapper.MapTo(r));
        }

        public RequestDTO GetById(int id)
        {
            Request foundRequest = unitOfWork.Requests.FindByID(id);
            if (foundRequest == null)
                return null;
            return requestMapper.MapTo(foundRequest);
           // return customMapper.MapOne<RequestDTO>(unitOfWork.Requests.FindByID(id));
        }

        public void Update(RequestDTO editedRequest)
        {
            Request request = unitOfWork.Requests.FindByID(editedRequest.Id);
            request.Status = (Status)editedRequest.Status;
            request.SpecialistId = editedRequest.SpecialistId;

            unitOfWork.Requests.Update(request);
            if (editedRequest.SpecialistId != null && editedRequest.Status == StatusModel.Processed)
                unitOfWork.Specialists.FindByID((int)editedRequest.SpecialistId).NumberOfProcessedRequests++;
            unitOfWork.Save();      
        }

        public void Delete(int id)
        {
            unitOfWork.Requests.Delete(id);
            unitOfWork.Save();
        }

        public int Count()
        {
            return unitOfWork.Requests.Count();
        }

        public IEnumerable<RequestDTO> GetAllWithDetails()
        {
            return unitOfWork.Requests.GetFiltered(new RequestsWithMessagesSpecification()).Select(r => requestMapper.MapTo(r));
        }
    }
}
