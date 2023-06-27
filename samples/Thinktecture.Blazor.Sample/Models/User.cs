using System.Text.Json.Serialization;

namespace Thinktecture.Blazor.Sample.Models
{
    public record User(
        [property: JsonPropertyName("id")] int Id,
        [property: JsonPropertyName("first_name")] string FirstName,
        [property: JsonPropertyName("last_name")] string LastName,
        [property: JsonPropertyName("email")] string Email,
        [property: JsonPropertyName("avatar")] string Avatar,
        [property: JsonPropertyName("phone_number")] string PhoneNumber,
        [property: JsonPropertyName("date_of_birth")] string DateOfBirth,
        [property: JsonPropertyName("credit_card")] CreditCard CreditCard,
        [property: JsonPropertyName("address")] Address Address
    );
}
