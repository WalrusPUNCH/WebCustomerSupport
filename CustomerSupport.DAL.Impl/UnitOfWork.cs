using CustomerSupport.DAL.Abstract;

namespace CustomerSupport.DAL.Impl
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CustomerSupportContext context;
        public UnitOfWork(CustomerSupportContext context)
        {
            this.context = context;
        }

        private IRequestRepository requestsRepository;
        private ISpecialistRepository specialistsRepository;
        public IRequestRepository Requests
        {
            get 
            {
                if (requestsRepository == null)
                    requestsRepository = new RequestRepository(context);
                return requestsRepository;
            }
        }

        public ISpecialistRepository Specialists
        {
            get
            {
                if (specialistsRepository == null)
                    specialistsRepository = new SpecialistRepository(context);
                return specialistsRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}