using System.Runtime.Serialization;

namespace Thinktecture.Blazor.GrpcDevTools.Shared.DTO;

[DataContract]
public class ConferenceDetailsRequest
{
    [DataMember(Order = 1)]
    public Guid ID { get; set; }
}