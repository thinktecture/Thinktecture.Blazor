using System.Text.Json.Serialization;

namespace Thinktecture.Blazor.Sample.Models
{
    public record CreditCard(
        [property: JsonPropertyName("cc_number")] string CcNumber
    );
}
