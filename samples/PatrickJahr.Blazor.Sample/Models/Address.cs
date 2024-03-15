using System.Text.Json.Serialization;

namespace PatrickJahr.Blazor.Sample.Models
{
    public record Address(
        [property: JsonPropertyName("city")] string City,
        [property: JsonPropertyName("street_name")] string StreetName,
        [property: JsonPropertyName("street_address")] string StreetAddress,
        [property: JsonPropertyName("zip_code")] string ZipCode,
        [property: JsonPropertyName("state")] string State,
        [property: JsonPropertyName("country")] string Country
    );
}
