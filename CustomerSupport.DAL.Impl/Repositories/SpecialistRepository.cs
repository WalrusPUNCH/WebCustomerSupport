using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

using CustomerSupport.DAL.Entities;
using CustomerSupport.DAL.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using CustomerSupport.DAL.Specifications;
using CustomerSupport.DAL.Specifications.Abstract;

namespace CustomerSupport.DAL.Impl
{
    public class SpecialistRepository : BaseRepository<Specialist>, ISpecialistRepository
    {
        private readonly CustomerSupportContext context;
        public SpecialistRepository(CustomerSupportContext context) : base(context)
        {
            this.context = context;
        }
        public override Specialist FindByID(int id)
        {
            return  context.Specialists.Include(s => s.ActiveRequests).FirstOrDefault(spec => spec.Id == id);
        }
        public override IEnumerable<Specialist> GetAll(int page, int pageSize)
        {
            var specialists = context.Specialists.Include(s => s.ActiveRequests).GetPaged(page, pageSize).ToList();
            return specialists;
        }
        public override void Delete(int id)
        {
            Specialist specialist = context.Specialists/*.Include(s => s.ActiveRequests)*/.FirstOrDefault(spec => spec.Id == id);
            if (specialist != null)
            {
                context.Specialists.Remove(specialist);
            }
        }
        public IEnumerable<Specialist> GetSpecialistsWithAmountOfRequestsAboveAvarage()
        {
            if (context.Specialists.Count() == 0)
                return null;
            double averageAmount = context.Specialists.Average(spec => spec.NumberOfProcessedRequests);
            IEnumerable<Specialist> specialists = context.Specialists.Where(spec => spec.NumberOfProcessedRequests > averageAmount).ToList();
            return specialists;
        }
        //IEnumerable<Specialist> ISpecialistRepository.GetSpecialistsWithNoActiveRequests()
        //{
        //    if (context.Specialists.Count() == 0)
        //        return null;
        //    IEnumerable<Specialist> specialists = context.Specialists.Where(spec => spec.ActiveRequests.Count == 0).ToList();
        //    return specialists;
        //}
        public Specialist GetTheLeastBusySpecialist()
        {
            if (context.Specialists.Count() == 0)
                return null;
            return context.Specialists.OrderBy(item => item.ActiveRequests.Count).ThenBy(item => item.NumberOfProcessedRequests).First();
        }

        public IEnumerable<Specialist> GetFiltered(ISpecification<Specialist> filter)
        {
            if (context.Specialists.Count() == 0)
                return null;
            return context.Specialists.Where(filter.Criteria).AsEnumerable();
        }
    }
}
