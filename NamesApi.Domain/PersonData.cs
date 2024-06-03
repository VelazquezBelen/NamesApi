using System.Text.Json.Serialization;

namespace NamesAPI.Domain
{
    public class PersonData
    {
        [JsonPropertyName("response")]
        public IEnumerable<Person>? Response { get; set; }
    }
}
