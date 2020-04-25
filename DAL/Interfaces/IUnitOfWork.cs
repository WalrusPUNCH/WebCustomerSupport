using DAL.Repositories;

namespace DAL.Interfaces
{
    public interface IUnitOfWork
    {
        RequestsRepository RequestsRepository { get; }
        SpecialistsRepository SpecialistsRepository { get; }
    }
}