using System.ComponentModel.DataAnnotations;

namespace NamesAPI.Domain
{
    public class PersonFilters
    {
        [RegularExpression("M|F", ErrorMessage = "The gender must be 'M' o 'F'")]
        public string? Gender { get; set; }
        public string? NameStartsWith { get; set; }
    }
}
