using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

using BLL.Models;
using BLL.Interfaces;
using DAL.Interfaces;

namespace BLL.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public StatisticsService(IUnitOfWork unit, IMapper mapper)
        {
            unitOfWork = unit;
            this.mapper = mapper;
        }
        public IEnumerable<SpecialistModel> GetSpecialistsWithAboveAvarageAmountOfRequests()
        {
            return mapper.Map<IEnumerable<SpecialistModel>>(unitOfWork.SpecialistsRepository.GetSpecialistsWithAboveAvarageAmountOfRequests());
        }
        public IEnumerable<SpecialistModel> GetSpecialistsWithNoActiveRequests()
        {
            return mapper.Map<IEnumerable<SpecialistModel>>(unitOfWork.SpecialistsRepository.GetSpecialistsWithNoActiveRequests());
        }
    }
}
