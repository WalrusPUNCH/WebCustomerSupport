using System;
using System.Collections.Generic;
using System.Linq;


using AutoMapper;

using CustomerSupport.DAL.Abstract;
using CustomerSupport.DAL.Entities;
using CustomerSupport.BL.DTOs;
using CustomerSupport.BL.Abstract;
using CustomerSupport.BL.Services.Mapper.Abstract;

namespace CustomerSupport.BL.Services
{
    public class RequestManagementService :  IRequestManagementService
    {
        private readonly IUnitOfWork unitOfWork;
        //private readonly IMapper mapper;
        private readonly IBLMapper customMapper;
        public RequestManagementService(IUnitOfWork unitOfWork,
                                        /* IMapper mapper,*/ 
                                        IBLMapper customMapper)
        {
         //   this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.customMapper = customMapper;
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
            return unitOfWork.Requests.GetLastId();
        }

        public IEnumerable<RequestDTO> GetAll(int page, int pageSize)
        {
            return customMapper.MapMany<RequestDTO>(unitOfWork.Requests.GetAll(page, pageSize));
        }

        public RequestDTO GetById(int id)
        {
            return customMapper.MapOne<RequestDTO>(unitOfWork.Requests.FindByID(id));
        }

        public void Update(RequestDTO editedRequest)
        {          
            unitOfWork.Requests.Update(customMapper.MapOne<Request>(editedRequest));
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
    }
}
