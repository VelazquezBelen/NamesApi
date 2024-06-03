using Microsoft.Extensions.Logging;
using Moq;
using NamesAPI.Application;
using NamesAPI.Domain;

namespace NamesAPI.Tests
{
    public class PersonServiceTests
    {
        private readonly Mock<IPersonRepository> _personRepositoryMock;
        private readonly Mock<ILogger<PersonService>> _loggerMock;
        private readonly PersonService _personService;
        public PersonServiceTests()
        {
            _personRepositoryMock = new Mock<IPersonRepository>();
            _loggerMock = new Mock<ILogger<PersonService>>();
            _personService = new PersonService(_personRepositoryMock.Object, _loggerMock.Object);
        }
        private static IEnumerable<Person> GetPersonTestList()
        {
            return new List<Person>
            {
                new Person { Name = "Adrian", Gender = "M" },
                new Person { Name = "Alejandro", Gender = "M" },
                new Person { Name = "Alvaro", Gender = "M" },
                new Person { Name = "Ludmila", Gender = "F" },
                new Person { Name = "Maria", Gender = "F" },
            };
        }        

        [Fact]
        public async Task GetByFilter_ShouldReturnAllPerson_WhenNullFilters()
        {
            // Arrange
            var persons = GetPersonTestList();
            _personRepositoryMock.Setup(repo => repo.GetAll())
                            .ReturnsAsync(persons);

            // Act
            var result = await _personService.GetByFilter(null);

            // Assert
            Assert.Equal(result.Count(), persons.Count());
        }

        [Fact]
        public async Task GetByFilter_ShouldReturnFilteredData_WhenGenderFilterIsSend()
        {
            // Arrange
            var persons = GetPersonTestList();
            _personRepositoryMock.Setup(repo => repo.GetAll())
                            .ReturnsAsync(persons);
            var filters = new PersonFilters { Gender = "M" };

            // Act
            var result = await _personService.GetByFilter(filters);

            // Assert
            Assert.All(result, person => Assert.Equal(filters.Gender, person.Gender));
        }
        [Fact]
        public async Task GetByFilter_ShouldReturnFilteredData_WhenStartsWithFilterIsSend()
        {
            // Arrange
            var persons = GetPersonTestList();
            _personRepositoryMock.Setup(repo => repo.GetAll())
                            .ReturnsAsync(persons);
            var filters = new PersonFilters { NameStartsWith = "Al" };

            // Act
            var result = await _personService.GetByFilter(filters);

            // Assert
            Assert.Equal(result.Count(), persons.Where(person => person.Name.StartsWith(filters.NameStartsWith)).Count());
            Assert.All(result, person => Assert.StartsWith(filters.NameStartsWith, person.Name));
        }

        [Fact]
        public async Task GetByFilter_ShouldThrowExpection_WhenRepositoryThrowsException()
        {
            // Arrange
            var filters = new PersonFilters { NameStartsWith = "Al" };
            _personRepositoryMock.Setup(repo => repo.GetAll())
                            .ThrowsAsync(new InvalidOperationException("Repository exception"));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _personService.GetByFilter(filters));
            Assert.Equal("Repository exception", exception.Message);
        }
    }
}
