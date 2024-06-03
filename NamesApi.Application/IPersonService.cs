using NamesAPI.Domain;

namespace NamesAPI.Application
{
    public interface IPersonService
    {
        Task<IEnumerable<Person>> GetByFilter(PersonFilters? filters);
    }
}
