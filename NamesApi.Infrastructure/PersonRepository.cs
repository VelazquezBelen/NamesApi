using Microsoft.Extensions.Logging;
using NamesAPI.Application;
using NamesAPI.Domain;
using System.Text.Json;

namespace NamesAPI.Infrastructure
{
    public class PersonRepository : IPersonRepository
    {
        private readonly string _dataFileName = "names.json";
        private readonly ILogger<PersonRepository> _logger;

        public PersonRepository(ILogger<PersonRepository> logger)
        {
            _logger = logger;
        }

        public async Task<IEnumerable<Person>> GetAll()
        {
            try
            {
                string personJsonData = await File.ReadAllTextAsync(_dataFileName);
                var json = JsonSerializer.Deserialize<PersonData>(personJsonData);
                return json?.Response ?? [];
            } 
            catch (JsonException ex)
            {
                _logger.LogError(ex, "An error occurred while deserializing JSON file");
                throw; 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while searching for the data");
                throw;
            }
        }
    }
}
