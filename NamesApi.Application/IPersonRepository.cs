using NamesAPI.Domain;

namespace NamesAPI.Application
{
    public interface IPersonRepository
    {
        Task<IEnumerable<Person>> GetAll();
    }
}
