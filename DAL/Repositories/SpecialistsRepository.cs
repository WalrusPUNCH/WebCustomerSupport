using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Storage;
using DAL.Interfaces;


namespace DAL.Repositories
{
    public class SpecialistsRepository : ISpecialistRepository
    {
        private static volatile int maxSpecialistId = 0;

        public int Insert(Specialist specialist)
        {
            var ID = Interlocked.Increment(ref maxSpecialistId);
            specialist.Id = ID;
            StaticStorage.Specialists.TryAdd(specialist.Id, specialist);
            return ID;
        }
        public Specialist GetById(int id)
        {
            if (StaticStorage.Specialists.TryGetValue(id, out Specialist specialist) == true)
                return specialist;
            else
                return null;
        }
        public IEnumerable<Specialist> GetAll()
        {
            return StaticStorage.Specialists.Values;
        }
        public void Update(Specialist specialist)
        {
            StaticStorage.Specialists[specialist.Id] = specialist;

        }
        public bool Delete(int id)
        {
            return StaticStorage.Specialists.TryRemove(id, out var specialist); // what is specialst init val would be
        }

        public Specialist GetSpecialistByName(string name)
        {
            foreach (Specialist specialist in StaticStorage.Specialists.Values)
                if (specialist.Name == name)
                    return specialist;
            return null;
        }

        public IEnumerable<Specialist> GetSpecialistsWithAboveAvarageAmountOfRequests() // later we can change this to IQuerable to get already filtrated data from DB
        {
            if (StaticStorage.Specialists.Values.Count == 0)
                return null;
            var averageAmountOfRequests = StaticStorage.Specialists.Values.Average(spec => spec.NumberOfProcessedRequests);
            return StaticStorage.Specialists.Values.Where(specialist => specialist.NumberOfProcessedRequests >= averageAmountOfRequests);
        }

        public IEnumerable<Specialist> GetSpecialistsWithNoActiveRequests()
        {
            return StaticStorage.Specialists.Values.Where(specialist => specialist.ActiveRequests.Count == 0);
        }

        public Specialist GetTheLeastBusySpecialist()
        {
            if (StaticStorage.Specialists.Values.Count == 0)
                return null;
            return StaticStorage.Specialists.Values.OrderBy(item => item.ActiveRequests.Count).First();
        }
    }
}
