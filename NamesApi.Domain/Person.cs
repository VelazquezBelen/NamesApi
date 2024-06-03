using System.Text.Json.Serialization;

namespace NamesAPI.Domain
{
    public class Person
    {
        [JsonPropertyName("name")]
        public required string Name { get; set; }
        [JsonPropertyName("gender")]
        public required string Gender { get; set; }
    }
}
