using System;

namespace CustomerSupport.DAL.Abstract
{
    public interface IUnitOfWork
    {
        IRequestRepository Requests { get; }
        ISpecialistRepository Specialists { get; }
        void Save();
    }
}