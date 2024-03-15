using System.Text.Json.Serialization;

namespace PatrickJahr.Blazor.Sample.Models
{
    public record CreditCard(
        [property: JsonPropertyName("cc_number")] string CcNumber
    );
}
