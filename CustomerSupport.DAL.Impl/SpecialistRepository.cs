using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

using CustomerSupport.DAL.Entities;
using CustomerSupport.DAL.Abstract;
using Microsoft.EntityFrameworkCore;

namespace CustomerSupport.DAL.Impl
{
    public class SpecialistRepository : ISpecialistRepository
    {
        private readonly CustomerSupportContext context;
        public SpecialistRepository(CustomerSupportContext context)
        {
            this.context = context;
        }

        public void Create(Specialist specialist)
        {
            context.Specialists.Add(specialist);
        }
        public Specialist GetById(int id)
        {
            Specialist specialist = context.Specialists.Include(s => s.ActiveRequests).AsNoTracking().Where(spec => spec.Id == id).First();
            if (specialist != null)
                return specialist;
            else
                return null;
        }
        public IEnumerable<Specialist> GetAll()
        {
            var specialists = context.Specialists.Include(s => s.ActiveRequests).AsNoTracking().ToList();
            return specialists;
        }
        public void Update(Specialist specialist)
        {
            context.Specialists.Update(specialist);
        }
        public bool Delete(int id)
        {
            Specialist specialist = context.Specialists.Include(s => s.ActiveRequests).AsNoTracking().Where(spec => spec.Id == id).First();
            if (specialist != null)
            {
                foreach (Request request in specialist.ActiveRequests)
                {
                    request.Status = Status.Queued;
                    context.Requests.Update(request);
                }
                context.Specialists.Remove(specialist);
                return true;
            }
            else
                return false;
        }
        public IEnumerable<Specialist> GetSpecialistsWithAmountOfRequestsAboveAvarage()
        {
            if (context.Specialists.Count() == 0)
                return null;
            double averageAmount = context.Specialists.Average(spec => spec.NumberOfProcessedRequests);
            IEnumerable<Specialist> specialists = context.Specialists.AsNoTracking().Where(spec => spec.NumberOfProcessedRequests > averageAmount).ToList();
            return specialists;
        }

        IEnumerable<Specialist> ISpecialistRepository.GetSpecialistsWithNoActiveRequests()
        {
            if (context.Specialists.Count() == 0)
                return null;
            IEnumerable<Specialist> specialists = context.Specialists.AsNoTracking().Where(spec => spec.ActiveRequests.Count == 0).ToList();
            return specialists;
        }
        public Specialist GetTheLeastBusySpecialist()
        {
            if (context.Specialists.Count() == 0)
                return null;
            return context.Specialists.AsNoTracking().OrderBy(item => item.ActiveRequests.Count).ThenBy(item => item.NumberOfProcessedRequests).First();
        }
    }
}
