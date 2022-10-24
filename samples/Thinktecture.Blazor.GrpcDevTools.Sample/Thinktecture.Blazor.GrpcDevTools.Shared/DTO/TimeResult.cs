using System.Runtime.Serialization;

namespace Thinktecture.Blazor.GrpcDevTools.Shared.DTO;

[DataContract]
public class TimeResult
{
    [DataMember(Order = 1)]
    public string Time { get; set; }
}