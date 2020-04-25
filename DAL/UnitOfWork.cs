using DAL.Interfaces;
using DAL.Repositories;

namespace DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private RequestsRepository requestsRepository;
        private SpecialistsRepository specialistsRepository;
        public RequestsRepository RequestsRepository
        {
            get 
            {
                if (requestsRepository == null)
                    requestsRepository = new RequestsRepository();
                return requestsRepository;
            }
        }

        public SpecialistsRepository SpecialistsRepository
        {
            get
            {
                if (specialistsRepository == null)
                    specialistsRepository = new SpecialistsRepository();
                return specialistsRepository;
            }
        }
    }
}