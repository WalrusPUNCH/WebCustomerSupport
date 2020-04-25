using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

using BLL.Interfaces;
using BLL.Models;
using DAL.Interfaces;

namespace BLL.Services
{
    public class RequestsInformationService : IGetRequestsService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public RequestsInformationService(IUnitOfWork unit, IMapper mapper)
        {
            unitOfWork = unit;
            this.mapper = mapper;
        }
        public IEnumerable<RequestModel> GetAll()
        {
            return mapper.Map<IEnumerable<RequestModel>>(unitOfWork.RequestsRepository.GetAll());
        }
    }
}
