using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Thinktecture.Blazor.GrpcDevTools.Shared.DTO;

[DataContract]
public class ConferenceDetailModel
{
    [DataMember(Order = 1)]
    public Guid ID { get; set; }

    [DataMember(Order = 2)]
    [Required]
    public string Title { get; set; }

    [DataMember(Order = 3)]
    [Required]
    public DateTime? DateFrom { get; set; }

    [DataMember(Order = 4)]
    [Required]
    public DateTime? DateTo { get; set; }

    [DataMember(Order = 5)]
    [Required]
    public string Country { get; set; }

    [DataMember(Order = 6)]
    [Required]
    public string City { get; set; }
}