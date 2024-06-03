using Microsoft.Extensions.Logging;
using NamesAPI.Domain;

namespace NamesAPI.Application
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;
        private readonly ILogger<PersonService> _logger;
        public PersonService(IPersonRepository personRepository, ILogger<PersonService> logger) { 
            _personRepository = personRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<Person>> GetByFilter(PersonFilters? filters)
        {           
            try
            {
                IEnumerable<Person> personList = new List<Person>();
                personList = await _personRepository.GetAll();
                if (filters == null) { return personList; }

                if (!string.IsNullOrEmpty(filters?.Gender)) {
                    personList = personList.Where(person => person.Gender == filters.Gender);                    
                }

                if (!string.IsNullOrEmpty(filters?.NameStartsWith))
                {
                    personList = personList.Where(person => person.Name.StartsWith(filters.NameStartsWith, StringComparison.InvariantCultureIgnoreCase));
                }
                _logger.LogInformation("The data was successfully obtained for the filters {filters}", filters);
                return personList;
            } 
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while searching for the data for {filters}", filters);
                throw;
            }        
        }
    }
}
